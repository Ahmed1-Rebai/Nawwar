using System.Collections.Concurrent;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum.Domain;
using Nawwar.Api.Infrastructure.Localization;
using Nawwar.Api.Infrastructure.Persistence;

namespace Nawwar.Api.Features.Curriculum;

public sealed record LessonAssetDto(
    string Url,
    string Format,
    float[] CameraPosition,
    float[] CameraTarget);

public sealed record LessonSummaryDto(
    string Slug,
    int Order,
    string Title,
    string Objective,
    string ModelUrl,
    string ModelFormat,
    int CheckpointCount);

public sealed record LearningModuleDto(
    string Slug,
    int Order,
    string Title,
    string Summary,
    IReadOnlyList<LessonSummaryDto> Lessons);

public sealed record CurriculumOverviewResponse(
    string Culture,
    string Direction,
    IReadOnlyList<LearningModuleDto> Modules);

public sealed record LessonCheckpointDto(
    int StepOrder,
    string TargetNodeName,
    float[] FocusPoint,
    string Title,
    string Explanation);

public sealed record LessonDetailResponse(
    string Slug,
    string ModuleSlug,
    string Culture,
    string Direction,
    string Title,
    string Objective,
    LessonAssetDto Asset,
    IReadOnlyList<LessonCheckpointDto> Checkpoints);

public sealed record CompleteCheckpointRequest(string LearnerKey);

public sealed record LessonProgressDto(
    string LessonSlug,
    IReadOnlyList<int> CompletedSteps,
    int TotalCheckpoints);

public sealed record LearnerProgressResponse(
    string LearnerKey,
    int TotalCompletedSteps,
    int TotalCheckpoints,
    IReadOnlyList<LessonProgressDto> Lessons);

public static class CurriculumEndpoints
{
    private const string DefaultLearnerKey = "default";
    private const int MaxLearnerKeyLength = 80;

    private static readonly ConcurrentDictionary<(string LearnerKey, string LessonSlug, int StepOrder), DateTimeOffset> TestingCompletions = new();

    public static RouteGroupBuilder MapCurriculumEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/curriculum").WithTags("Curriculum");

        group.MapGet("/modules", GetModulesAsync)
            .WithName("GetCurriculumModules")
            .WithSummary("Lists all curriculum modules and their interactive 3D (.glb) lessons in the active request culture.");

        group.MapGet("/lessons/{slug}", GetLessonBySlugAsync)
            .WithName("GetCurriculumLessonBySlug")
            .WithSummary("Returns a single interactive 3D (.glb) lesson and its scene checkpoints in the active request culture.");

        group.MapGet("/progress", GetLearnerProgressAsync)
            .WithName("GetLearnerProgress")
            .WithSummary("Returns completed 3D scene checkpoints across all curriculum lessons for a learner.");

        group.MapPost("/lessons/{slug}/checkpoints/{stepOrder:int}/complete", CompleteCheckpointAsync)
            .WithName("CompleteLessonCheckpoint")
            .WithSummary("Marks a 3D scene checkpoint as completed for a learner and returns updated curriculum progress.");

        return group;
    }

    internal static async Task<Ok<CurriculumOverviewResponse>> GetModulesAsync(
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentUICulture;
        var language = NormalizeLanguage(culture);

        var modules = await LoadModulesAsync(db, environment, cancellationToken);

        var mapped = modules
            .OrderBy(m => m.DisplayOrder)
            .Select(m => new LearningModuleDto(
                Slug: m.Slug,
                Order: m.DisplayOrder,
                Title: SelectText(language, m.TitleAr, m.TitleFr, m.TitleEn),
                Summary: SelectText(language, m.SummaryAr, m.SummaryFr, m.SummaryEn),
                Lessons: m.Lessons
                    .OrderBy(l => l.DisplayOrder)
                    .Select(l => new LessonSummaryDto(
                        Slug: l.Slug,
                        Order: l.DisplayOrder,
                        Title: SelectText(language, l.TitleAr, l.TitleFr, l.TitleEn),
                        Objective: SelectText(language, l.ObjectiveAr, l.ObjectiveFr, l.ObjectiveEn),
                        ModelUrl: l.ModelUrl,
                        ModelFormat: l.ModelFormat,
                        CheckpointCount: l.Checkpoints.Count))
                    .ToList()))
            .ToList();

        return TypedResults.Ok(new CurriculumOverviewResponse(
            Culture: language,
            Direction: LocalizationSetup.GetTextDirection(culture),
            Modules: mapped));
    }

    internal static async Task<Results<Ok<LessonDetailResponse>, ProblemHttpResult>> GetLessonBySlugAsync(
        string slug,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentUICulture;
        var language = NormalizeLanguage(culture);

        var modules = await LoadModulesAsync(db, environment, cancellationToken);
        var match = modules
            .SelectMany(m => m.Lessons.Select(l => (Module: m, Lesson: l)))
            .FirstOrDefault(x => string.Equals(x.Lesson.Slug, slug, StringComparison.OrdinalIgnoreCase));

        if (match.Lesson is null)
        {
            return TypedResults.Problem(
                title: "Lesson not found",
                detail: $"No interactive 3D lesson exists with slug '{slug}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        var lesson = match.Lesson;
        var detail = new LessonDetailResponse(
            Slug: lesson.Slug,
            ModuleSlug: match.Module.Slug,
            Culture: language,
            Direction: LocalizationSetup.GetTextDirection(culture),
            Title: SelectText(language, lesson.TitleAr, lesson.TitleFr, lesson.TitleEn),
            Objective: SelectText(language, lesson.ObjectiveAr, lesson.ObjectiveFr, lesson.ObjectiveEn),
            Asset: new LessonAssetDto(
                Url: lesson.ModelUrl,
                Format: lesson.ModelFormat,
                CameraPosition: [lesson.CameraPositionX, lesson.CameraPositionY, lesson.CameraPositionZ],
                CameraTarget: [lesson.CameraTargetX, lesson.CameraTargetY, lesson.CameraTargetZ]),
            Checkpoints: lesson.Checkpoints
                .OrderBy(c => c.StepOrder)
                .Select(c => new LessonCheckpointDto(
                    StepOrder: c.StepOrder,
                    TargetNodeName: c.TargetNodeName,
                    FocusPoint: [c.FocusX, c.FocusY, c.FocusZ],
                    Title: SelectText(language, c.TitleAr, c.TitleFr, c.TitleEn),
                    Explanation: SelectText(language, c.ExplanationAr, c.ExplanationFr, c.ExplanationEn)))
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

        var modules = await LoadModulesAsync(db, environment, cancellationToken);
        var progress = await BuildLearnerProgressAsync(normalizedKey, modules, db, environment, cancellationToken);
        return TypedResults.Ok(progress);
    }

    internal static async Task<Results<Ok<LearnerProgressResponse>, ProblemHttpResult>> CompleteCheckpointAsync(
        string slug,
        int stepOrder,
        CompleteCheckpointRequest request,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.LearnerKey))
        {
            return TypedResults.Problem(
                title: "Invalid learner key",
                detail: "A non-empty learnerKey is required to record checkpoint completion.",
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

        var modules = await LoadModulesAsync(db, environment, cancellationToken);
        var lesson = modules
            .SelectMany(m => m.Lessons)
            .FirstOrDefault(l => string.Equals(l.Slug, slug, StringComparison.OrdinalIgnoreCase));

        if (lesson is null)
        {
            return TypedResults.Problem(
                title: "Lesson not found",
                detail: $"No interactive 3D lesson exists with slug '{slug}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        var checkpoint = lesson.Checkpoints.FirstOrDefault(c => c.StepOrder == stepOrder);
        if (checkpoint is null)
        {
            return TypedResults.Problem(
                title: "Checkpoint not found",
                detail: $"Lesson '{lesson.Slug}' does not contain checkpoint step {stepOrder}.",
                statusCode: StatusCodes.Status404NotFound);
        }

        if (environment.IsEnvironment("Testing"))
        {
            TestingCompletions.TryAdd((normalizedKey, lesson.Slug, checkpoint.StepOrder), DateTimeOffset.UtcNow);
        }
        else
        {
            var exists = await db.CheckpointCompletions.AnyAsync(
                c => c.LearnerKey == normalizedKey
                    && c.LessonSlug == lesson.Slug
                    && c.StepOrder == checkpoint.StepOrder,
                cancellationToken);

            if (!exists)
            {
                db.CheckpointCompletions.Add(new CheckpointCompletion
                {
                    LearnerKey = normalizedKey,
                    LessonSlug = lesson.Slug,
                    StepOrder = checkpoint.StepOrder,
                    CompletedAtUtc = DateTimeOffset.UtcNow,
                });
                await db.SaveChangesAsync(cancellationToken);
            }
        }

        var progress = await BuildLearnerProgressAsync(normalizedKey, modules, db, environment, cancellationToken);
        return TypedResults.Ok(progress);
    }

    private static async Task<LearnerProgressResponse> BuildLearnerProgressAsync(
        string learnerKey,
        IReadOnlyList<LearningModule> modules,
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        List<(string LessonSlug, int StepOrder)> completedPairs;

        if (environment.IsEnvironment("Testing"))
        {
            completedPairs = TestingCompletions.Keys
                .Where(k => string.Equals(k.LearnerKey, learnerKey, StringComparison.Ordinal))
                .Select(k => (k.LessonSlug, k.StepOrder))
                .ToList();
        }
        else
        {
            var rows = await db.CheckpointCompletions
                .AsNoTracking()
                .Where(c => c.LearnerKey == learnerKey)
                .Select(c => new { c.LessonSlug, c.StepOrder })
                .ToListAsync(cancellationToken);

            completedPairs = rows.Select(r => (r.LessonSlug, r.StepOrder)).ToList();
        }

        var allLessons = modules
            .OrderBy(m => m.DisplayOrder)
            .SelectMany(m => m.Lessons.OrderBy(l => l.DisplayOrder))
            .ToList();

        var lessonDtos = allLessons
            .Select(l =>
            {
                var steps = completedPairs
                    .Where(p => string.Equals(p.LessonSlug, l.Slug, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p.StepOrder)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                return new LessonProgressDto(
                    LessonSlug: l.Slug,
                    CompletedSteps: steps,
                    TotalCheckpoints: l.Checkpoints.Count);
            })
            .ToList();

        return new LearnerProgressResponse(
            LearnerKey: learnerKey,
            TotalCompletedSteps: lessonDtos.Sum(l => l.CompletedSteps.Count),
            TotalCheckpoints: lessonDtos.Sum(l => l.TotalCheckpoints),
            Lessons: lessonDtos);
    }

    private static async Task<IReadOnlyList<LearningModule>> LoadModulesAsync(
        NawwarDbContext db,
        IHostEnvironment environment,
        CancellationToken cancellationToken)
    {
        if (environment.IsEnvironment("Testing"))
        {
            return CurriculumCatalog.CreateSeedModules();
        }

        return await db.LearningModules
            .AsNoTracking()
            .Include(m => m.Lessons)
            .ThenInclude(l => l.Checkpoints)
            .OrderBy(m => m.DisplayOrder)
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
