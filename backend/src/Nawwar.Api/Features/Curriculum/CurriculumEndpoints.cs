using System.Collections.Concurrent;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum.Domain;
using Nawwar.Api.Infrastructure.Localization;
using Nawwar.Api.Infrastructure.Persistence;

namespace Nawwar.Api.Features.Curriculum;

public sealed record AgeGroupSummaryDto(
    int AgeYears,
    string Slug,
    string ThemeKey,
    string IconEmoji,
    string Title,
    string Subtitle,
    string Description,
    int DomainCount,
    int CourseCount,
    int ActivityCount);

public sealed record AgeGroupsOverviewResponse(
    string Culture,
    string Direction,
    IReadOnlyList<AgeGroupSummaryDto> AgeGroups);

public sealed record LearningObjectiveDto(
    string Code,
    int Order,
    string Description);

public sealed record LessonSummaryDto(
    string Slug,
    int Order,
    int EstimatedMinutes,
    string Title,
    string Summary,
    int ActivityCount,
    bool Has3DScene);

public sealed record CourseSummaryDto(
    string Slug,
    int TargetAge,
    int Order,
    int DifficultyLevel,
    string IconEmoji,
    string Title,
    string Description,
    string CompletionCriteria,
    IReadOnlyList<LearningObjectiveDto> Objectives,
    IReadOnlyList<LessonSummaryDto> Lessons);

public sealed record CourseCollectionDto(
    string Slug,
    int Order,
    bool IsReadingPathway,
    string Title,
    string Summary,
    IReadOnlyList<CourseSummaryDto> Courses);

public sealed record LearningDomainDto(
    string Slug,
    int Order,
    string IconEmoji,
    string AccentColor,
    string Title,
    string Summary,
    IReadOnlyList<CourseCollectionDto> Collections);

public sealed record AgeGroupDetailResponse(
    int AgeYears,
    string Slug,
    string ThemeKey,
    string IconEmoji,
    string Culture,
    string Direction,
    string Title,
    string Subtitle,
    string Description,
    IReadOnlyList<LearningDomainDto> Domains);

public sealed record ActivityDto(
    string Slug,
    int StepOrder,
    string ActivityType,
    string ObjectiveCode,
    string Title,
    string Prompt,
    string SpokenText,
    string? GlbModelUrl,
    string? TargetNodeName,
    float[] FocusPoint,
    string InteractionPayloadJson);

public sealed record LessonDetailResponse(
    string Slug,
    string CourseSlug,
    string CourseTitle,
    int AgeYears,
    string Culture,
    string Direction,
    string Title,
    string Summary,
    int EstimatedMinutes,
    IReadOnlyList<ActivityDto> Activities);

public sealed record CompleteActivityRequest(string LearnerKey);

public sealed record AgeProgressSummaryDto(
    int AgeYears,
    string Title,
    int CompletedActivities,
    int TotalActivities);

public sealed record LearnerProgressResponse(
    string LearnerKey,
    int TotalCompletedActivities,
    int TotalActivities,
    IReadOnlyList<string> CompletedActivitySlugs,
    IReadOnlyList<AgeProgressSummaryDto> ByAge);

public static class CurriculumEndpoints
{
    private const string DefaultLearnerKey = "default";
    private const int MaxLearnerKeyLength = 80;

    private static readonly ConcurrentDictionary<(string LearnerKey, string ActivitySlug), DateTimeOffset> TestingCompletions = new();

    public static RouteGroupBuilder MapCurriculumEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/curriculum").WithTags("Curriculum");

        group.MapGet("/age-groups", GetAgeGroupsAsync)
            .WithName("GetAgeGroups")
            .WithSummary("Lists the three preschool age worlds (Ages 3, 4, and 5) and their summary counts.");

        group.MapGet("/age-groups/{ageYears:int}", GetAgeGroupDetailAsync)
            .WithName("GetAgeGroupDetail")
            .WithSummary("Returns the full learning domains, course collections, courses, objectives, and lesson summaries for a preschool age group (3, 4, or 5).");

        group.MapGet("/lessons/{slug}", GetLessonBySlugAsync)
            .WithName("GetCurriculumLessonBySlug")
            .WithSummary("Returns a preschool lesson and its ordered multi-modal activities in the active request culture.");

        group.MapGet("/progress", GetLearnerProgressAsync)
            .WithName("GetLearnerProgress")
            .WithSummary("Returns completed activities and per-age progress summary for a learner.");

        group.MapPost("/activities/{activitySlug}/complete", CompleteActivityAsync)
            .WithName("CompleteCurriculumActivity")
            .WithSummary("Marks an activity as completed for a learner and returns updated curriculum progress.");

        return group;
    }

    internal static async Task<Ok<AgeGroupsOverviewResponse>> GetAgeGroupsAsync(
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentUICulture;
        var language = NormalizeLanguage(culture);

        var ageGroups = await LoadAgeGroupsAsync(db, environment, cancellationToken);

        var mapped = ageGroups
            .OrderBy(a => a.DisplayOrder)
            .Select(a =>
            {
                var courses = a.Domains
                    .SelectMany(d => d.Collections)
                    .SelectMany(c => c.Courses)
                    .ToList();
                var activityCount = courses
                    .SelectMany(c => c.Lessons)
                    .SelectMany(l => l.Activities)
                    .Count();

                return new AgeGroupSummaryDto(
                    AgeYears: a.AgeYears,
                    Slug: a.Slug,
                    ThemeKey: a.ThemeKey,
                    IconEmoji: a.IconEmoji,
                    Title: SelectText(language, a.TitleAr, a.TitleFr, a.TitleEn),
                    Subtitle: SelectText(language, a.SubtitleAr, a.SubtitleFr, a.SubtitleEn),
                    Description: SelectText(language, a.DescriptionAr, a.DescriptionFr, a.DescriptionEn),
                    DomainCount: a.Domains.Count,
                    CourseCount: courses.Count,
                    ActivityCount: activityCount);
            })
            .ToList();

        return TypedResults.Ok(new AgeGroupsOverviewResponse(
            Culture: language,
            Direction: LocalizationSetup.GetTextDirection(culture),
            AgeGroups: mapped));
    }

    internal static async Task<Results<Ok<AgeGroupDetailResponse>, ProblemHttpResult>> GetAgeGroupDetailAsync(
        int ageYears,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentUICulture;
        var language = NormalizeLanguage(culture);

        var ageGroups = await LoadAgeGroupsAsync(db, environment, cancellationToken);
        var ageGroup = ageGroups.FirstOrDefault(a => a.AgeYears == ageYears);

        if (ageGroup is null)
        {
            return TypedResults.Problem(
                title: "Age group not found",
                detail: $"No preschool curriculum world exists for age '{ageYears}'. Supported ages are 3, 4, and 5.",
                statusCode: StatusCodes.Status404NotFound);
        }

        var domains = ageGroup.Domains
            .OrderBy(d => d.DisplayOrder)
            .Select(d => new LearningDomainDto(
                Slug: d.Slug,
                Order: d.DisplayOrder,
                IconEmoji: d.IconEmoji,
                AccentColor: d.AccentColor,
                Title: SelectText(language, d.TitleAr, d.TitleFr, d.TitleEn),
                Summary: SelectText(language, d.SummaryAr, d.SummaryFr, d.SummaryEn),
                Collections: d.Collections
                    .OrderBy(c => c.DisplayOrder)
                    .Select(c => new CourseCollectionDto(
                        Slug: c.Slug,
                        Order: c.DisplayOrder,
                        IsReadingPathway: c.IsReadingPathway,
                        Title: SelectText(language, c.TitleAr, c.TitleFr, c.TitleEn),
                        Summary: SelectText(language, c.SummaryAr, c.SummaryFr, c.SummaryEn),
                        Courses: c.Courses
                            .OrderBy(co => co.DisplayOrder)
                            .Select(co => new CourseSummaryDto(
                                Slug: co.Slug,
                                TargetAge: co.TargetAge,
                                Order: co.DisplayOrder,
                                DifficultyLevel: co.DifficultyLevel,
                                IconEmoji: co.IconEmoji,
                                Title: SelectText(language, co.TitleAr, co.TitleFr, co.TitleEn),
                                Description: SelectText(language, co.DescriptionAr, co.DescriptionFr, co.DescriptionEn),
                                CompletionCriteria: SelectText(language, co.CompletionCriteriaAr, co.CompletionCriteriaFr, co.CompletionCriteriaEn),
                                Objectives: co.Objectives
                                    .OrderBy(o => o.DisplayOrder)
                                    .Select(o => new LearningObjectiveDto(
                                        Code: o.Code,
                                        Order: o.DisplayOrder,
                                        Description: SelectText(language, o.DescriptionAr, o.DescriptionFr, o.DescriptionEn)))
                                    .ToList(),
                                Lessons: co.Lessons
                                    .OrderBy(l => l.DisplayOrder)
                                    .Select(l => new LessonSummaryDto(
                                        Slug: l.Slug,
                                        Order: l.DisplayOrder,
                                        EstimatedMinutes: l.EstimatedMinutes,
                                        Title: SelectText(language, l.TitleAr, l.TitleFr, l.TitleEn),
                                        Summary: SelectText(language, l.SummaryAr, l.SummaryFr, l.SummaryEn),
                                        ActivityCount: l.Activities.Count,
                                        Has3DScene: l.Activities.Any(act => !string.IsNullOrEmpty(act.GlbModelUrl))))
                                    .ToList()))
                            .ToList()))
                    .ToList()))
            .ToList();

        return TypedResults.Ok(new AgeGroupDetailResponse(
            AgeYears: ageGroup.AgeYears,
            Slug: ageGroup.Slug,
            ThemeKey: ageGroup.ThemeKey,
            IconEmoji: ageGroup.IconEmoji,
            Culture: language,
            Direction: LocalizationSetup.GetTextDirection(culture),
            Title: SelectText(language, ageGroup.TitleAr, ageGroup.TitleFr, ageGroup.TitleEn),
            Subtitle: SelectText(language, ageGroup.SubtitleAr, ageGroup.SubtitleFr, ageGroup.SubtitleEn),
            Description: SelectText(language, ageGroup.DescriptionAr, ageGroup.DescriptionFr, ageGroup.DescriptionEn),
            Domains: domains));
    }

    internal static async Task<Results<Ok<LessonDetailResponse>, ProblemHttpResult>> GetLessonBySlugAsync(
        string slug,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentUICulture;
        var language = NormalizeLanguage(culture);

        var ageGroups = await LoadAgeGroupsAsync(db, environment, cancellationToken);
        var match = ageGroups
            .SelectMany(ag => ag.Domains)
            .SelectMany(d => d.Collections)
            .SelectMany(c => c.Courses.SelectMany(co => co.Lessons.Select(l => (Course: co, Lesson: l))))
            .FirstOrDefault(x => string.Equals(x.Lesson.Slug, slug, StringComparison.OrdinalIgnoreCase));

        if (match.Lesson is null)
        {
            return TypedResults.Problem(
                title: "Lesson not found",
                detail: $"No preschool lesson exists with slug '{slug}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        var lesson = match.Lesson;
        var course = match.Course;

        var detail = new LessonDetailResponse(
            Slug: lesson.Slug,
            CourseSlug: course.Slug,
            CourseTitle: SelectText(language, course.TitleAr, course.TitleFr, course.TitleEn),
            AgeYears: course.TargetAge,
            Culture: language,
            Direction: LocalizationSetup.GetTextDirection(culture),
            Title: SelectText(language, lesson.TitleAr, lesson.TitleFr, lesson.TitleEn),
            Summary: SelectText(language, lesson.SummaryAr, lesson.SummaryFr, lesson.SummaryEn),
            EstimatedMinutes: lesson.EstimatedMinutes,
            Activities: lesson.Activities
                .OrderBy(a => a.StepOrder)
                .Select(a => new ActivityDto(
                    Slug: a.Slug,
                    StepOrder: a.StepOrder,
                    ActivityType: a.ActivityType,
                    ObjectiveCode: a.ObjectiveCode,
                    Title: SelectText(language, a.TitleAr, a.TitleFr, a.TitleEn),
                    Prompt: SelectText(language, a.PromptAr, a.PromptFr, a.PromptEn),
                    SpokenText: SelectText(language, a.SpokenTextAr, a.SpokenTextFr, a.SpokenTextEn),
                    GlbModelUrl: a.GlbModelUrl,
                    TargetNodeName: a.TargetNodeName,
                    FocusPoint: [a.FocusX, a.FocusY, a.FocusZ],
                    InteractionPayloadJson: a.InteractionPayloadJson))
                .ToList());

        return TypedResults.Ok(detail);
    }

    internal static async Task<Results<Ok<LearnerProgressResponse>, ProblemHttpResult>> GetLearnerProgressAsync(
        string? learnerKey,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var normalizedKey = string.IsNullOrWhiteSpace(learnerKey) ? DefaultLearnerKey : learnerKey.Trim();
        if (normalizedKey.Length > MaxLearnerKeyLength)
        {
            return TypedResults.Problem(
                title: "Invalid learner key",
                detail: $"Learner key must not exceed {MaxLearnerKeyLength} characters.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var ageGroups = await LoadAgeGroupsAsync(db, environment, cancellationToken);
        var progress = await BuildLearnerProgressAsync(normalizedKey, ageGroups, db, environment, cancellationToken);
        return TypedResults.Ok(progress);
    }

    internal static async Task<Results<Ok<LearnerProgressResponse>, ProblemHttpResult>> CompleteActivityAsync(
        string activitySlug,
        CompleteActivityRequest request,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.LearnerKey))
        {
            return TypedResults.Problem(
                title: "Invalid learner key",
                detail: "A non-empty learnerKey is required to record activity completion.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var normalizedKey = request.LearnerKey.Trim();
        if (normalizedKey.Length > MaxLearnerKeyLength)
        {
            return TypedResults.Problem(
                title: "Invalid learner key",
                detail: $"Learner key must not exceed {MaxLearnerKeyLength} characters.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var ageGroups = await LoadAgeGroupsAsync(db, environment, cancellationToken);
        var match = ageGroups
            .SelectMany(ag => ag.Domains)
            .SelectMany(d => d.Collections)
            .SelectMany(c => c.Courses)
            .SelectMany(co => co.Lessons.SelectMany(l => l.Activities.Select(a => (Course: co, Lesson: l, Activity: a))))
            .FirstOrDefault(x => string.Equals(x.Activity.Slug, activitySlug, StringComparison.OrdinalIgnoreCase));

        if (match.Activity is null)
        {
            return TypedResults.Problem(
                title: "Activity not found",
                detail: $"No curriculum activity exists with slug '{activitySlug}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        if (environment.IsEnvironment("Testing"))
        {
            TestingCompletions.TryAdd((normalizedKey, match.Activity.Slug), DateTimeOffset.UtcNow);
        }
        else
        {
            var exists = await db.ActivityCompletions.AnyAsync(
                c => c.LearnerKey == normalizedKey && c.ActivitySlug == match.Activity.Slug,
                cancellationToken);

            if (!exists)
            {
                db.ActivityCompletions.Add(new ActivityCompletion
                {
                    LearnerKey = normalizedKey,
                    AgeYears = match.Course.TargetAge,
                    CourseSlug = match.Course.Slug,
                    LessonSlug = match.Lesson.Slug,
                    ActivitySlug = match.Activity.Slug,
                    CompletedAtUtc = DateTimeOffset.UtcNow,
                });
                await db.SaveChangesAsync(cancellationToken);
            }
        }

        var progress = await BuildLearnerProgressAsync(normalizedKey, ageGroups, db, environment, cancellationToken);
        return TypedResults.Ok(progress);
    }

    private static async Task<LearnerProgressResponse> BuildLearnerProgressAsync(
        string learnerKey,
        IReadOnlyList<AgeGroup> ageGroups,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var language = NormalizeLanguage(CultureInfo.CurrentUICulture);
        HashSet<string> completedSlugs;

        if (environment.IsEnvironment("Testing"))
        {
            completedSlugs = TestingCompletions.Keys
                .Where(k => string.Equals(k.LearnerKey, learnerKey, StringComparison.Ordinal))
                .Select(k => k.ActivitySlug)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
        else
        {
            var slugs = await db.ActivityCompletions
                .AsNoTracking()
                .Where(c => c.LearnerKey == learnerKey)
                .Select(c => c.ActivitySlug)
                .ToListAsync(cancellationToken);

            completedSlugs = slugs.ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        var byAge = ageGroups
            .OrderBy(a => a.DisplayOrder)
            .Select(a =>
            {
                var activities = a.Domains
                    .SelectMany(d => d.Collections)
                    .SelectMany(c => c.Courses)
                    .SelectMany(co => co.Lessons)
                    .SelectMany(l => l.Activities)
                    .ToList();

                var completedCount = activities.Count(act => completedSlugs.Contains(act.Slug));

                return new AgeProgressSummaryDto(
                    AgeYears: a.AgeYears,
                    Title: SelectText(language, a.TitleAr, a.TitleFr, a.TitleEn),
                    CompletedActivities: completedCount,
                    TotalActivities: activities.Count);
            })
            .ToList();

        return new LearnerProgressResponse(
            LearnerKey: learnerKey,
            TotalCompletedActivities: byAge.Sum(x => x.CompletedActivities),
            TotalActivities: byAge.Sum(x => x.TotalActivities),
            CompletedActivitySlugs: completedSlugs.OrderBy(s => s, StringComparer.Ordinal).ToList(),
            ByAge: byAge);
    }

    private static async Task<IReadOnlyList<AgeGroup>> LoadAgeGroupsAsync(
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        if (environment.IsEnvironment("Testing"))
        {
            return CurriculumCatalog.CreateSeedAgeGroups();
        }

        return await db.AgeGroups
            .AsNoTracking()
            .Include(a => a.Domains)
                .ThenInclude(d => d.Collections)
                    .ThenInclude(c => c.Courses)
                        .ThenInclude(co => co.Objectives)
            .Include(a => a.Domains)
                .ThenInclude(d => d.Collections)
                    .ThenInclude(c => c.Courses)
                        .ThenInclude(co => co.Lessons)
                            .ThenInclude(l => l.Activities)
            .AsSplitQuery()
            .OrderBy(a => a.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    private static string NormalizeLanguage(CultureInfo culture) =>
        culture.TwoLetterISOLanguageName switch
        {
            "fr" => "fr",
            "en" => "en",
            _ => "ar",
        };

    private static string SelectText(string language, string ar, string fr, string en) =>
        language switch
        {
            "fr" => fr,
            "en" => en,
            _ => ar,
        };
}
