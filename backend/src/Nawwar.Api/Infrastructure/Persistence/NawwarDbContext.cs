using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Infrastructure.Persistence;

/// <summary>
/// Primary EF Core context for PostgreSQL. Entity configurations are discovered
/// automatically from <see cref="IEntityTypeConfiguration{TEntity}"/> implementations in this assembly.
/// </summary>
public sealed class NawwarDbContext(DbContextOptions<NawwarDbContext> options) : DbContext(options)
{
    public DbSet<LearningModule> LearningModules => Set<LearningModule>();

    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<LessonCheckpoint> LessonCheckpoints => Set<LessonCheckpoint>();

    public DbSet<CheckpointCompletion> CheckpointCompletions => Set<CheckpointCompletion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NawwarDbContext).Assembly);
    }
}
