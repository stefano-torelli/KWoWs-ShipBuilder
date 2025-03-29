using Microsoft.Extensions.Options;
using WoWsShipBuilder.Infrastructure.ApplicationData;
using WoWsShipBuilder.Infrastructure.Localization;

namespace WoWsShipBuilder.Web.Infrastructure.Data;

public class DataInitializer(IOptions<CdnOptions> cdnOptions, ILocalizationProvider localizationProvider, IAppDataService appDataService)
{
    private readonly CdnOptions cdnOptions = cdnOptions.Value;

    public async Task InitializeData()
    {
        await localizationProvider.RefreshDataAsync(this.cdnOptions.Server, [.. AppConstants.SupportedLanguages]);
        if (appDataService is ServerAppDataService serverAppDataService)
        {
            if (this.cdnOptions.UseLocalFiles)
            {
                await serverAppDataService.LoadLocalFilesAsync(this.cdnOptions.Server);
            }
            else
            {
                await serverAppDataService.FetchData();
            }
        }
    }
}
