namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Records that a learner completed an <see cref="Activity"/> within a preschool lesson and course.
/// Designed to work without authentication in early phases and bind to learner profiles in PHASE H.
/// </summary>
public sealed class ActivityCompletion
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string LearnerKey { get; init; }

    public required int AgeYears { get; init; }

    public required string CourseSlug { get; init; }

    public required string LessonSlug { get; init; }

    public required string ActivitySlug { get; init; }

    public DateTimeOffset CompletedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
