using WoWsShipBuilder.Features.Settings;
using WoWsShipBuilder.Infrastructure.DataTransfer;
using WoWsShipBuilder.Infrastructure.Localization.Resources;

namespace WoWsShipBuilder.Infrastructure.Localization;

public class Localizer(ILocalizationProvider gameLocalizationProvider, AppSettings appSettings) : ILocalizer
{
    private readonly ILocalizationProvider gameLocalizationProvider = gameLocalizationProvider;

    private readonly AppSettings appSettings = appSettings;

    public LocalizationResult GetGameLocalization(string key) => this.GetGameLocalization(key, this.appSettings.SelectedLanguage);

    public LocalizationResult GetGameLocalization(string key, CultureDetails language)
    {
        var result = this.gameLocalizationProvider.GetString(key, language);
        return new(result != null, result ?? key);
    }

    public LocalizationResult GetAppLocalization(string key) => this.GetAppLocalization(key, this.appSettings.SelectedLanguage);

    public LocalizationResult GetAppLocalization(string key, params object[] args)
    {
        var localization = this.GetAppLocalization(key);
        return localization with { Localization = string.Format(this.appSettings.SelectedLanguage.CultureInfo, localization.Localization, args) };
    }

    public LocalizationResult GetAppLocalization(string key, CultureDetails language)
    {
        if (this.appSettings.EnableLocalizationDebugMode)
        {
            return new(true, key);
        }

        var result = Translation.ResourceManager.GetString(key, language.CultureInfo);
        return new(result != null, result ?? key);
    }

    public LocalizationResult GetAppLocalization(string key, CultureDetails language, params object[] args)
    {
        var localization = this.GetAppLocalization(key, language);
        return localization with { Localization = string.Format(language.CultureInfo, localization.Localization, args) };
    }
}
