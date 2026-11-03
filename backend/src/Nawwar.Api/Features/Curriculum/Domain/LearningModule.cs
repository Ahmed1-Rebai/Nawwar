namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// A curriculum module grouping related interactive 3D lessons.
/// Localized text is stored for Arabic (primary, with diacritics), French, and English.
/// </summary>
public sealed class LearningModule
{
    public Guid Id { get; init; }

    public required string Slug { get; init; }

    public int DisplayOrder { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string SummaryAr { get; init; }

    public required string SummaryFr { get; init; }

    public required string SummaryEn { get; init; }

    public ICollection<Lesson> Lessons { get; init; } = [];
}
