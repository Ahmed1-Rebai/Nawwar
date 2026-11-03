namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 7 of the NAWWAR curriculum hierarchy: a measurable early-childhood learning objective
/// (e.g., <c>L3-CL-01</c>, <c>L5-RD-03</c>) linked to a <see cref="Course"/> and reinforced by activities.
/// </summary>
public sealed class LearningObjective
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid CourseId { get; init; }

    public Course? Course { get; init; }

    public required string Code { get; init; }

    public required int DisplayOrder { get; init; }

    public required string DescriptionAr { get; init; }

    public required string DescriptionFr { get; init; }

    public required string DescriptionEn { get; init; }
}
