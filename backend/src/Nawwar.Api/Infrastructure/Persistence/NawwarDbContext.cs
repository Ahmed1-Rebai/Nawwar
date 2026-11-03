using Microsoft.EntityFrameworkCore;

namespace Nawwar.Api.Infrastructure.Persistence;

/// <summary>
/// Primary EF Core context for PostgreSQL. No domain entities exist yet; the
/// domain model will be added once the product/educational model is designed
/// (see ROADMAP.md, Phase 1). Entity configurations are discovered automatically
/// from <see cref="IEntityTypeConfiguration{TEntity}"/> implementations in this assembly.
/// </summary>
public sealed class NawwarDbContext(DbContextOptions<NawwarDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NawwarDbContext).Assembly);
    }
}

