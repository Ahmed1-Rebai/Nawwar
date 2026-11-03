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

        if (!await db.LearningModules.AnyAsync(cancellationToken))
        {
            var seedModules = CurriculumCatalog.CreateSeedModules();
            db.LearningModules.AddRange(seedModules);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
