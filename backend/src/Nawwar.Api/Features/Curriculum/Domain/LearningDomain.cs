namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 2 of the NAWWAR curriculum hierarchy: a learning domain within an <see cref="AgeGroup"/>
/// (e.g., Arabic Language &amp; Reading, Colors &amp; Shapes, Counting &amp; Early Math, Animals &amp; Nature).
/// </summary>
public sealed class LearningDomain
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid AgeGroupId { get; init; }

    public AgeGroup? AgeGroup { get; init; }

    public required string Slug { get; init; }

    public required int DisplayOrder { get; init; }

    public required string IconEmoji { get; init; }

    public required string AccentColor { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string SummaryAr { get; init; }

    public required string SummaryFr { get; init; }

    public required string SummaryEn { get; init; }

    public ICollection<CourseCollection> Collections { get; init; } = new List<CourseCollection>();
}
