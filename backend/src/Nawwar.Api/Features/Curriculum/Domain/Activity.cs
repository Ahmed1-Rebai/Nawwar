namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 6 of the NAWWAR curriculum hierarchy: an interactive, multi-modal learning activity
/// inside a <see cref="Lesson"/>. Supports 3D exploration (<c>scene_3d_explore</c>), audio listening
/// (<c>audio_listen_pick</c>), sorting/matching (<c>visual_match_sort</c>), counting (<c>counting_interactive</c>),
/// Arabic letter forms &amp; short vowels (<c>letter_forms_vowels</c>), and syllable/word/sentence reading (<c>syllable_word_builder</c>).
/// </summary>
public sealed class Activity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid LessonId { get; init; }

    public Lesson? Lesson { get; init; }

    public required string Slug { get; init; }

    public required int StepOrder { get; init; }

    public required string ActivityType { get; init; }

    public required string ObjectiveCode { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string PromptAr { get; init; }

    public required string PromptFr { get; init; }

    public required string PromptEn { get; init; }

    public required string SpokenTextAr { get; init; }

    public required string SpokenTextFr { get; init; }

    public required string SpokenTextEn { get; init; }

    /// <summary>
    /// Optional binary glTF 2.0 (<c>.glb</c>) scene URL when <see cref="ActivityType"/> uses a 3D environment.
    /// </summary>
    public string? GlbModelUrl { get; init; }

    /// <summary>
    /// Optional target 3D scene-graph node highlighted or picked during a 3D activity.
    /// </summary>
    public string? TargetNodeName { get; init; }

    public float FocusX { get; init; }

    public float FocusY { get; init; }

    public float FocusZ { get; init; }

    /// <summary>
    /// Data-driven JSON configuration for interactive options (cards, letter forms, vowels, syllables, or counting items).
    /// </summary>
    public required string InteractionPayloadJson { get; init; }
}
