using System.Net;
using System.Net.Http.Json;
using Nawwar.Api.Features.Curriculum;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Tests;

public sealed class CurriculumEndpointsTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task AgeGroups_DefaultsToArabic_AndReturnsThreePreschoolWorlds()
    {
        var overview = await _client.GetFromJsonAsync<AgeGroupsOverviewResponse>(
            new Uri("/api/v1/curriculum/age-groups", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.NotNull(overview);
        Assert.Equal("ar", overview.Culture);
        Assert.Equal("rtl", overview.Direction);
        Assert.Equal(3, overview.AgeGroups.Count);

        Assert.Equal([3, 4, 5], overview.AgeGroups.Select(a => a.AgeYears).ToArray());
        Assert.Equal("عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ", overview.AgeGroups[0].Title);
        Assert.Equal("عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ", overview.AgeGroups[1].Title);
        Assert.Equal("عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ", overview.AgeGroups[2].Title);
    }

    [Fact]
    public async Task AgeGroupDetail_ForAge5_IncludesSixCourseArabicReadingPathway()
    {
        var age5 = await _client.GetFromJsonAsync<AgeGroupDetailResponse>(
            new Uri("/api/v1/curriculum/age-groups/5", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.NotNull(age5);
        Assert.Equal(5, age5.AgeYears);
        Assert.Equal("ar", age5.Culture);

        var arabicDomain = Assert.Single(age5.Domains, d => d.Slug == "age-5-arabic-language-and-reading");
        Assert.Equal("اللُّغَةُ العَرَبِيَّةُ", arabicDomain.Title);

        var readingPathway = Assert.Single(arabicDomain.Collections, c => c.IsReadingPathway);
        Assert.Equal("مَسَارُ تَعَلُّمِ القِرَاءَةِ", readingPathway.Title);
        Assert.Equal(6, readingPathway.Courses.Count);

        Assert.Equal(
            [
                "اِكْتِشَافُ الحُرُوفِ",
                "أَشْكَالُ الحُرُوفِ",
                "الحَرَكَاتُ القَصِيرَةُ",
                "بِنَاءُ المَقَاطِعِ",
                "قِرَاءَةُ الكَلِمَاتِ",
                "قِرَاءَةُ الجُمَلِ البَسِيطَةِ",
            ],
            readingPathway.Courses.Select(c => c.Title).ToArray());
    }

    [Theory]
    [InlineData("fr-FR", "fr", "ltr", "La pomme rouge et la fleur jaune dans le jardin")]
    [InlineData("en-US", "en", "ltr", "The Red Apple and Yellow Flower in the Garden")]
    [InlineData("ar-TN", "ar", "rtl", "التُّفَّاحَةُ الحَمْرَاءُ وَالزَّهْرَةُ الصَّفْرَاءُ فِي الحَدِيقَةِ")]
    public async Task LessonBySlug_ReturnsMultiModalActivities_AndEnforcesGlbFor3DScenes(
        string acceptLanguage,
        string expectedCulture,
        string expectedDirection,
        string expectedTitle)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            new Uri("/api/v1/curriculum/lessons/red-apple-yellow-sun-garden", UriKind.Relative));
        request.Headers.AcceptLanguage.ParseAdd(acceptLanguage);

        using var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);
        var detail = await response.Content.ReadFromJsonAsync<LessonDetailResponse>(TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(detail);
        Assert.Equal(3, detail.AgeYears);
        Assert.Equal(expectedCulture, detail.Culture);
        Assert.Equal(expectedDirection, detail.Direction);
        Assert.Equal(expectedTitle, detail.Title);
        Assert.Equal(2, detail.Activities.Count);

        var sceneActivity = detail.Activities[0];
        Assert.Equal("scene_3d_explore", sceneActivity.ActivityType);
        Assert.True(Lesson.IsValidGlbUrl(sceneActivity.GlbModelUrl));
        Assert.Equal("/models/garden-colors.glb", sceneActivity.GlbModelUrl);

        var audioActivity = detail.Activities[1];
        Assert.Equal("audio_listen_pick", audioActivity.ActivityType);
        Assert.Null(audioActivity.GlbModelUrl);
    }

    [Fact]
    public async Task Progress_RecordsActivityCompletionIdempotently_AndSummarizesByAge()
    {
        var learnerKey = $"learner-{Guid.NewGuid():N}";

        var initial = await _client.GetFromJsonAsync<LearnerProgressResponse>(
            new Uri($"/api/v1/curriculum/progress?learnerKey={learnerKey}", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.NotNull(initial);
        Assert.Equal(0, initial.TotalCompletedActivities);
        Assert.Equal(3, initial.ByAge.Count);

        using var completeResponse = await _client.PostAsJsonAsync(
            new Uri("/api/v1/curriculum/activities/age5-rd4-build-kataba/complete", UriKind.Relative),
            new CompleteActivityRequest(learnerKey),
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, completeResponse.StatusCode);
        var updated = await completeResponse.Content.ReadFromJsonAsync<LearnerProgressResponse>(TestContext.Current.CancellationToken);

        Assert.NotNull(updated);
        Assert.Equal(1, updated.TotalCompletedActivities);
        Assert.Contains("age5-rd4-build-kataba", updated.CompletedActivitySlugs);

        var age5Progress = Assert.Single(updated.ByAge, a => a.AgeYears == 5);
        Assert.Equal(1, age5Progress.CompletedActivities);

        // Idempotent repeat call
        using var duplicateResponse = await _client.PostAsJsonAsync(
            new Uri("/api/v1/curriculum/activities/age5-rd4-build-kataba/complete", UriKind.Relative),
            new CompleteActivityRequest(learnerKey),
            TestContext.Current.CancellationToken);

        var afterDuplicate = await duplicateResponse.Content.ReadFromJsonAsync<LearnerProgressResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(afterDuplicate);
        Assert.Equal(1, afterDuplicate.TotalCompletedActivities);
    }

    [Fact]
    public async Task AgeGroupDetail_ReturnsProblemDetails_WhenAgeIsUnsupported()
    {
        using var response = await _client.GetAsync(
            new Uri("/api/v1/curriculum/age-groups/9", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
