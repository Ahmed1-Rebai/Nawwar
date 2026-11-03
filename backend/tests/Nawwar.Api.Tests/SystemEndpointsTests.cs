using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Nawwar.Api.Features.System;

namespace Nawwar.Api.Tests;

/// <summary>
/// In-memory integration tests. They do not require PostgreSQL: only endpoints that
/// never open a database connection are exercised here. Database-backed behaviour is
/// validated through Docker Compose (see docs/infrastructure/DOCKER.md).
/// </summary>
public sealed class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        // Placeholder only — never used to connect in these tests.
        builder.UseSetting("ConnectionStrings:Default", "Host=unused;Database=unused;Username=unused;Password=unused");
    }
}

public sealed class SystemEndpointsTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Liveness_ReturnsHealthy_WithoutDatabase()
    {
        var response = await _client.GetAsync(new Uri("/health/live", UriKind.Relative), TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Info_DefaultsToArabic_RightToLeft()
    {
        var info = await _client.GetFromJsonAsync<SystemInfoResponse>(
            new Uri("/api/v1/system/info", UriKind.Relative), TestContext.Current.CancellationToken);

        Assert.NotNull(info);
        Assert.Equal("NAWWAR", info.Name);
        Assert.Equal("ar", info.Culture);
        Assert.Equal("rtl", info.Direction);
        Assert.Equal("ar", info.DefaultCulture);
        Assert.Equal(["ar", "fr", "en"], info.SupportedCultures);
    }

    [Theory]
    [InlineData("fr-FR,fr;q=0.9", "fr", "ltr")]
    [InlineData("en-US,en;q=0.8", "en", "ltr")]
    [InlineData("ar-MA,ar;q=0.9", "ar", "rtl")]
    [InlineData("de-DE", "ar", "rtl")] // unsupported -> Arabic default
    public async Task Info_ResolvesCultureFromAcceptLanguage(string acceptLanguage, string expectedCulture, string expectedDirection)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, new Uri("/api/v1/system/info", UriKind.Relative));
        request.Headers.AcceptLanguage.ParseAdd(acceptLanguage);

        using var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);
        var info = await response.Content.ReadFromJsonAsync<SystemInfoResponse>(TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(info);
        Assert.Equal(expectedCulture, info.Culture);
        Assert.Equal(expectedDirection, info.Direction);
        Assert.Equal(expectedCulture, response.Content.Headers.ContentLanguage.Single());
    }

    [Fact]
    public async Task UnknownRoute_ReturnsProblemDetails()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, new Uri("/api/v1/does-not-exist", UriKind.Relative));
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}

