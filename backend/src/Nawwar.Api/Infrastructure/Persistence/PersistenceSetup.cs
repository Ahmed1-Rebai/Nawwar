using Microsoft.EntityFrameworkCore;

namespace Nawwar.Api.Infrastructure.Persistence;

public static class PersistenceSetup
{
    public const string ConnectionStringName = "Default";

    /// <summary>
    /// Registers <see cref="NawwarDbContext"/> with Npgsql. The connection string is
    /// read from <c>ConnectionStrings:Default</c> (env: <c>ConnectionStrings__Default</c>),
    /// which Docker Compose builds from <c>.env</c>. It is resolved lazily so that the
    /// process can start (and report liveness) even when configuration is incomplete;
    /// the readiness health check surfaces the problem.
    /// </summary>
    public static IServiceCollection AddNawwarPersistence(this IServiceCollection services)
    {
        services.AddDbContext<NawwarDbContext>((serviceProvider, options) =>
        {
            var connectionString = serviceProvider.GetRequiredService<IConfiguration>()
                .GetConnectionString(ConnectionStringName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string '{ConnectionStringName}' is not configured. " +
                    "Set ConnectionStrings__Default (Docker Compose builds it from .env).");
            }

            options.UseNpgsql(connectionString, npgsql => npgsql.EnableRetryOnFailure());
        });

        return services;
    }
}

