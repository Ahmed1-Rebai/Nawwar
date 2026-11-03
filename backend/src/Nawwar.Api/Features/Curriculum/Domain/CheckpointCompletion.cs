namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Records that a learner identified by <see cref="LearnerKey"/> has completed
/// a specific 3D scene checkpoint (<see cref="StepOrder"/>) within a lesson (<see cref="LessonSlug"/>).
/// </summary>
public sealed class CheckpointCompletion
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string LearnerKey { get; init; }

    public required string LessonSlug { get; init; }

    public required int StepOrder { get; init; }

    public DateTimeOffset CompletedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}

