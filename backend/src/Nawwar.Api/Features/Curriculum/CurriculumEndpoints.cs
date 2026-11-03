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

public static class CurriculumEndpoints
{
    public static RouteGroupBuilder MapCurriculumEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/curriculum").WithTags("Curriculum");

        group.MapGet("/modules", GetModulesAsync)
            .WithName("GetCurriculumModules")
            .WithSummary("Lists all curriculum modules and their interactive 3D (.glb) lessons in the active request culture.");

        group.MapGet("/lessons/{slug}", GetLessonBySlugAsync)
            .WithName("GetCurriculumLessonBySlug")
            .WithSummary("Returns a single interactive 3D (.glb) lesson and its scene checkpoints in the active request culture.");

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
