using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Nawwar.Api.Features.Curriculum;
using Nawwar.Api.Features.System;
using Nawwar.Api.Infrastructure.Localization;
using Nawwar.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddNawwarLocalization();
builder.Services.AddNawwarPersistence();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<NawwarDbContext>("database", tags: ["ready"]);

// The API is only reachable through the frontend reverse proxy (or directly in
// development). Honour X-Forwarded-* from the proxy so scheme/client IP are correct.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // The proxy's address is assigned dynamically on the Docker network.
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    // OpenAPI document: /openapi/v1.json (development only).
    app.MapOpenApi();
}

// Liveness: the process is up. Never touches dependencies.
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
// Readiness: dependencies (PostgreSQL) are reachable. Used by Docker health checks.
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
});

var api = app.MapGroup("/api/v1");
api.MapSystemEndpoints();
api.MapCurriculumEndpoints();

await app.InitializeCurriculumAsync();
await app.RunAsync();

/// <summary>Entry point; public partial for <c>WebApplicationFactory</c> in integration tests.</summary>
public partial class Program;

