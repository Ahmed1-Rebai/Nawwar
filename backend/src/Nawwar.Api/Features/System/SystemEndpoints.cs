using System.Globalization;
using System.Reflection;
using Nawwar.Api.Infrastructure.Localization;

namespace Nawwar.Api.Features.System;

/// <summary>Response of <c>GET /api/v1/system/info</c>.</summary>
public sealed record SystemInfoResponse(
    string Name,
    string Version,
    string Culture,
    string Direction,
    string DefaultCulture,
    IReadOnlyList<string> SupportedCultures);

public static class SystemEndpoints
{
    public static RouteGroupBuilder MapSystemEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/system").WithTags("System");

        group.MapGet("/info", GetInfo)
            .WithName("GetSystemInfo")
            .WithSummary("Service metadata and the culture resolved for this request.");

        return group;
    }

    internal static SystemInfoResponse GetInfo()
    {
        var culture = CultureInfo.CurrentUICulture;
        return new SystemInfoResponse(
            Name: "NAWWAR",
            Version: ApplicationVersion,
            Culture: culture.Name,
            Direction: LocalizationSetup.GetTextDirection(culture),
            DefaultCulture: LocalizationSetup.DefaultCulture,
            SupportedCultures: LocalizationSetup.SupportedCultures);
    }

    private static readonly string ApplicationVersion =
        typeof(SystemEndpoints).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion.Split('+')[0]
        ?? "0.0.0";
}

