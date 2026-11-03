namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// An interactive inspection step inside a 3D <c>.glb</c> lesson scene.
/// <see cref="TargetNodeName"/> references a named node inside the <c>.glb</c> scene graph.
/// </summary>
public sealed class LessonCheckpoint
{
    public Guid Id { get; init; }

    public Guid LessonId { get; init; }

    public Lesson? Lesson { get; init; }

    public int StepOrder { get; init; }

    /// <summary>Name of the Object3D / Mesh node inside the <c>.glb</c> asset.</summary>
    public required string TargetNodeName { get; init; }

    public float FocusX { get; init; }

    public float FocusY { get; init; }

    public float FocusZ { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string ExplanationAr { get; init; }

    public required string ExplanationFr { get; init; }

    public required string ExplanationEn { get; init; }
}

