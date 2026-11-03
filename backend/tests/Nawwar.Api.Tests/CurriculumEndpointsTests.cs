using System.Net;
using System.Net.Http.Json;
using Nawwar.Api.Features.Curriculum;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Tests;

public sealed class CurriculumEndpointsTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Modules_DefaultsToArabic_AndAllLessonsUseGlbFormat()
    {
        var overview = await _client.GetFromJsonAsync<CurriculumOverviewResponse>(
            new Uri("/api/v1/curriculum/modules", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.NotNull(overview);
        Assert.Equal("ar", overview.Culture);
        Assert.Equal("rtl", overview.Direction);
        Assert.Equal(3, overview.Modules.Count);

        var firstModule = overview.Modules[0];
        Assert.Equal("optics-and-light", firstModule.Slug);
        Assert.Equal("البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ", firstModule.Title);

        var allLessons = overview.Modules.SelectMany(m => m.Lessons).ToList();
        Assert.Equal(3, allLessons.Count);
        Assert.All(allLessons, lesson =>
        {
            Assert.True(Lesson.IsValidGlbUrl(lesson.ModelUrl));
            Assert.EndsWith(".glb", lesson.ModelUrl, StringComparison.OrdinalIgnoreCase);
            Assert.Equal("model/gltf-binary", lesson.ModelFormat);
            Assert.Equal(3, lesson.CheckpointCount);
        });
    }

    [Theory]
    [InlineData("fr-FR", "fr", "ltr", "Le système Soleil-Terre-Lune")]
    [InlineData("en-US", "en", "ltr", "The Sun, Earth, and Moon System")]
    [InlineData("ar-TN", "ar", "rtl", "مَنْظُومَةُ الشَّمْسِ وَالأَرْضِ وَالقَمَرِ")]
    public async Task LessonBySlug_ReturnsLocalizedDetail_AndGlbCheckpoints(
        string acceptLanguage,
        string expectedCulture,
        string expectedDirection,
        string expectedTitle)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            new Uri("/api/v1/curriculum/lessons/sun-earth-moon-system", UriKind.Relative));
        request.Headers.AcceptLanguage.ParseAdd(acceptLanguage);

        using var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);
        var detail = await response.Content.ReadFromJsonAsync<LessonDetailResponse>(TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(detail);
        Assert.Equal("sun-earth-moon-system", detail.Slug);
        Assert.Equal("planetary-astronomy", detail.ModuleSlug);
        Assert.Equal(expectedCulture, detail.Culture);
        Assert.Equal(expectedDirection, detail.Direction);
        Assert.Equal(expectedTitle, detail.Title);
        Assert.Equal("/models/solar-system.glb", detail.Asset.Url);
        Assert.Equal("model/gltf-binary", detail.Asset.Format);
        Assert.Equal(3, detail.Checkpoints.Count);
        Assert.Equal(["Core_Sun", "Planet_Earth", "Moon_Satellite"], detail.Checkpoints.Select(c => c.TargetNodeName).ToArray());
    }

    [Fact]
    public async Task LessonBySlug_ReturnsProblemDetails_WhenSlugDoesNotExist()
    {
        using var response = await _client.GetAsync(
            new Uri("/api/v1/curriculum/lessons/unknown-lesson", UriKind.Relative),
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
