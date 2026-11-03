using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Infrastructure.Persistence;

/// <summary>
/// Primary EF Core context for PostgreSQL 18 backing the 7-level preschool curriculum
/// (<see cref="AgeGroup"/> → <see cref="LearningDomain"/> → <see cref="CourseCollection"/> →
/// <see cref="Course"/> → <see cref="Lesson"/> → <see cref="Activity"/> → <see cref="LearningObjective"/>)
/// and learner activity progress (<see cref="ActivityCompletion"/>).
/// </summary>
public sealed class NawwarDbContext(DbContextOptions<NawwarDbContext> options) : DbContext(options)
{
    public DbSet<AgeGroup> AgeGroups => Set<AgeGroup>();

    public DbSet<LearningDomain> LearningDomains => Set<LearningDomain>();

    public DbSet<CourseCollection> CourseCollections => Set<CourseCollection>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<LearningObjective> LearningObjectives => Set<LearningObjective>();

    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<Activity> Activities => Set<Activity>();

    public DbSet<ActivityCompletion> ActivityCompletions => Set<ActivityCompletion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NawwarDbContext).Assembly);
    }
}
