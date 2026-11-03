using System.Globalization;

namespace Nawwar.Api.Infrastructure.Localization;

/// <summary>
/// Arabic-first request localization. Arabic is the default culture; French and
/// English are supported. Culture is resolved from (in order) the <c>culture</c>
/// query string, the ASP.NET Core culture cookie, then <c>Accept-Language</c>.
/// Keep this list in sync with the frontend (<c>frontend/src/i18n/config.ts</c>).
/// </summary>
public static class LocalizationSetup
{
    public const string DefaultCulture = "ar";

    public static readonly IReadOnlyList<string> SupportedCultures = ["ar", "fr", "en"];

    public static IServiceCollection AddNawwarLocalization(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = SupportedCultures.ToArray();
            options.SetDefaultCulture(DefaultCulture)
                .AddSupportedCultures(cultures)
                .AddSupportedUICultures(cultures);
            options.ApplyCurrentCultureToResponseHeaders = true;
        });
        return services;
    }

    /// <summary>Text direction for a culture, used by clients that render server-provided text.</summary>
    public static string GetTextDirection(CultureInfo culture) =>
        culture.TextInfo.IsRightToLeft ? "rtl" : "ltr";
}

