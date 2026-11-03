namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// An interactive 3D lesson backed by a binary glTF 2.0 (<c>.glb</c>) scene asset.
/// </summary>
public sealed class Lesson
{
    public const string GlbMimeType = "model/gltf-binary";
    public const string GlbExtension = ".glb";

    public Guid Id { get; init; }

    public Guid ModuleId { get; init; }

    public LearningModule? Module { get; init; }

    public required string Slug { get; init; }

    public int DisplayOrder { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string ObjectiveAr { get; init; }

    public required string ObjectiveFr { get; init; }

    public required string ObjectiveEn { get; init; }

    /// <summary>Relative or S3 path to the binary glTF 2.0 (<c>.glb</c>) 3D model.</summary>
    public required string ModelUrl { get; init; }

    /// <summary>MIME type of the 3D asset; always <c>model/gltf-binary</c> (<c>.glb</c>).</summary>
    public string ModelFormat { get; init; } = GlbMimeType;

    public float CameraPositionX { get; init; } = 3.2f;

    public float CameraPositionY { get; init; } = 2.4f;

    public float CameraPositionZ { get; init; } = 4.2f;

    public float CameraTargetX { get; init; }

    public float CameraTargetY { get; init; } = 0.8f;

    public float CameraTargetZ { get; init; }

    public ICollection<LessonCheckpoint> Checkpoints { get; init; } = [];

    public static bool IsValidGlbUrl(string? url) =>
        !string.IsNullOrWhiteSpace(url)
        && url.EndsWith(GlbExtension, StringComparison.OrdinalIgnoreCase);
}

