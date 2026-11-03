namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 4 of the NAWWAR curriculum hierarchy: a structured preschool course containing
/// learning objectives, difficulty progression, completion criteria, and lessons.
/// </summary>
public sealed class Course
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid CollectionId { get; init; }

    public CourseCollection? Collection { get; init; }

    public required string Slug { get; init; }

    public required int TargetAge { get; init; }

    public required int DisplayOrder { get; init; }

    public required int DifficultyLevel { get; init; }

    public required string IconEmoji { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string DescriptionAr { get; init; }

    public required string DescriptionFr { get; init; }

    public required string DescriptionEn { get; init; }

    public required string CompletionCriteriaAr { get; init; }

    public required string CompletionCriteriaFr { get; init; }

    public required string CompletionCriteriaEn { get; init; }

    public ICollection<LearningObjective> Objectives { get; init; } = new List<LearningObjective>();

    public ICollection<Lesson> Lessons { get; init; } = new List<Lesson>();
}
