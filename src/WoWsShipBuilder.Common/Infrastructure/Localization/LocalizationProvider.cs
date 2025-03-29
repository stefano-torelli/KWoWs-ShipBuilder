using WoWsShipBuilder.Infrastructure.ApplicationData;
using WoWsShipBuilder.Infrastructure.DataTransfer;
using WoWsShipBuilder.Infrastructure.GameData;

namespace WoWsShipBuilder.Infrastructure.Localization;

public class LocalizationProvider(IAppDataService appDataService) : ILocalizationProvider
{
    private readonly Dictionary<CultureDetails, Dictionary<string, string>> localizationData = [];

    private readonly IAppDataService appDataService = appDataService;

    public async Task RefreshDataAsync(ServerType serverType, params CultureDetails[] supportedCultures)
    {
        foreach (var culture in supportedCultures)
        {
            var cultureLocalization = await this.appDataService.ReadLocalizationData(serverType, culture.LocalizationFileName) ?? throw new InvalidOperationException("Localization data not found");
            this.localizationData[culture] = cultureLocalization;
        }
    }

    public string? GetString(string key, CultureDetails cultureDetails)
    {
        if (!this.localizationData.TryGetValue(cultureDetails, out var cultureLocalization))
        {
            return null;
        }

        cultureLocalization.TryGetValue(key.ToUpperInvariant(), out var localization);
        return localization;
    }
}
