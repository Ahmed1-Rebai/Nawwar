namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 1 of the NAWWAR curriculum hierarchy: represents one of the three preschool
/// age worlds (Age 3: عَالَمُ الاِكْتِشَافِ, Age 4: عَالَمُ الاِسْتِكْشَافِ, Age 5: عَالَمُ التَّعَلُّمِ).
/// </summary>
public sealed class AgeGroup
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required int AgeYears { get; init; }

    public required string Slug { get; init; }

    public required string ThemeKey { get; init; }

    public required string IconEmoji { get; init; }

    public required int DisplayOrder { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string SubtitleAr { get; init; }

    public required string SubtitleFr { get; init; }

    public required string SubtitleEn { get; init; }

    public required string DescriptionAr { get; init; }

    public required string DescriptionFr { get; init; }

    public required string DescriptionEn { get; init; }

    public ICollection<LearningDomain> Domains { get; init; } = new List<LearningDomain>();
}
