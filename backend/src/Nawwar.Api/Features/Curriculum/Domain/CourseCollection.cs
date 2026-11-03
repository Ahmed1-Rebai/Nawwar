using System.Diagnostics.CodeAnalysis;

namespace Nawwar.Api.Features.Curriculum.Domain;

/// <summary>
/// Level 3 of the NAWWAR curriculum hierarchy: an ordered collection or pathway of courses
/// within a <see cref="LearningDomain"/> (such as Age 5's dedicated <c>مَسَارُ تَعَلُّمِ القِرَاءَةِ</c>).
/// </summary>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Domain entity representing a curriculum CourseCollection.")]
public sealed class CourseCollection
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Guid DomainId { get; init; }

    public LearningDomain? Domain { get; init; }

    public required string Slug { get; init; }

    public required int DisplayOrder { get; init; }

    public bool IsReadingPathway { get; init; }

    public required string TitleAr { get; init; }

    public required string TitleFr { get; init; }

    public required string TitleEn { get; init; }

    public required string SummaryAr { get; init; }

    public required string SummaryFr { get; init; }

    public required string SummaryEn { get; init; }

    public ICollection<Course> Courses { get; init; } = new List<Course>();
}
