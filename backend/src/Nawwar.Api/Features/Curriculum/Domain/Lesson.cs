namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 5 of the NAWWAR curriculum hierarchy: a pedagogical lesson within a <see cref="Course"/>.
/// Lessons are decoupled from any single 3D model; each lesson sequences one or more multi-modal <see cref="Activity"/> steps.
/// </summary>
public sealed class Lesson
{
    public const string GlbExtension = ".glb";
    public const string GlbMimeType = "model/gltf-binary";

    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid CourseId { get; init; }

    public Course? Course { get; init; }

    public required string Slug { get; init; }

    public required int DisplayOrder { get; init; }

    public required int EstimatedMinutes { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string SummaryAr { get; init; }

    public required string SummaryFr { get; init; }

    public required string SummaryEn { get; init; }

    public ICollection<Activity> Activities { get; init; } = new List<Activity>();

    public static bool IsValidGlbUrl(string? url) =>
        !string.IsNullOrWhiteSpace(url) &&
        url.Trim().EndsWith(GlbExtension, StringComparison.OrdinalIgnoreCase);
}
