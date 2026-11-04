using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum;

namespace Nawwar.Api.Infrastructure.Persistence;

/// <summary>
/// Ensures the PostgreSQL 7-level preschool curriculum schema exists (<c>age_groups</c>,
/// <c>learning_domains</c>, <c>course_collections</c>, <c>courses</c>, <c>learning_objectives</c>,
/// <c>lessons</c>, <c>activities</c>, <c>activity_completions</c>) and seeds the Ages 3, 4, and 5
/// curriculum on application startup.
/// </summary>
public static class CurriculumInitializer
{
    public static async Task InitializeCurriculumAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsEnvironment("Testing"))
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NawwarDbContext>();

        // Upgrade pre-PHASE C volumes that had the legacy learning_modules prototype schema
        // before creating the 7-level age-based curriculum tables.
        await db.Database.ExecuteSqlRawAsync(
            """
            DO $$
            BEGIN
                IF NOT EXISTS (
                    SELECT 1 FROM information_schema.tables
                    WHERE table_schema = 'public' AND table_name = 'age_groups'
                ) THEN
                    DROP TABLE IF EXISTS checkpoint_completions CASCADE;
                    DROP TABLE IF EXISTS lesson_checkpoints CASCADE;
                    DROP TABLE IF EXISTS lessons CASCADE;
                    DROP TABLE IF EXISTS learning_modules CASCADE;
                END IF;
            END
            $$;
            """,
            cancellationToken);

        await db.Database.EnsureCreatedAsync(cancellationToken);

        var seedAgeGroups = CurriculumCatalog.CreateSeedAgeGroups();
        var expectedActivityCount = seedAgeGroups
            .SelectMany(a => a.Domains)
            .SelectMany(d => d.Collections)
            .SelectMany(c => c.Courses)
            .SelectMany(cr => cr.Lessons)
            .Sum(l => l.Activities.Count);

        var currentActivityCount = await db.Activities.CountAsync(cancellationToken);
        var hasUpdatedWordExamples = await db.Activities.AnyAsync(
            a => a.InteractionPayloadJson.Contains("wordExamples"),
            cancellationToken);

        if (currentActivityCount != expectedActivityCount || !hasUpdatedWordExamples)
        {
            await db.AgeGroups.ExecuteDeleteAsync(cancellationToken);
            db.AgeGroups.AddRange(seedAgeGroups);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
