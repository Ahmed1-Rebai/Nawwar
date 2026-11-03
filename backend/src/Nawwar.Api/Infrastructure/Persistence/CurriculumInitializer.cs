using Microsoft.EntityFrameworkCore;
using Nawwar.Api.Features.Curriculum;

namespace Nawwar.Api.Infrastructure.Persistence;

/// <summary>
/// Ensures the PostgreSQL curriculum schema exists and seeds the initial Arabic-first
/// 3D (<c>.glb</c>) curriculum modules on application startup.
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

        await db.Database.EnsureCreatedAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS checkpoint_completions (
                id uuid NOT NULL PRIMARY KEY,
                learner_key character varying(80) NOT NULL,
                lesson_slug character varying(100) NOT NULL,
                step_order integer NOT NULL,
                completed_at_utc timestamp with time zone NOT NULL
            );
            CREATE UNIQUE INDEX IF NOT EXISTS ix_checkpoint_completions_learner_lesson_step
                ON checkpoint_completions (learner_key, lesson_slug, step_order);
            """,
            cancellationToken);

        if (!await db.LearningModules.AnyAsync(cancellationToken))
        {
            var seedModules = CurriculumCatalog.CreateSeedModules();
            db.LearningModules.AddRange(seedModules);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}

