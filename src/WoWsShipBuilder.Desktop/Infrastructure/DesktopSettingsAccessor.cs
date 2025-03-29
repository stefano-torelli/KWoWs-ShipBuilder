using System.Globalization;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Microsoft.Extensions.Logging;
using WoWsShipBuilder.Desktop.Infrastructure.Data;
using WoWsShipBuilder.Features.Settings;
using WoWsShipBuilder.Infrastructure.ApplicationData;

namespace WoWsShipBuilder.Desktop.Infrastructure;

public class DesktopSettingsAccessor(IAppDataService appDataService, IDataService dataService, IFileSystem fileSystem, ILogger<DesktopSettingsAccessor> logger) : ISettingsAccessor
{
    private readonly string settingsFile = dataService.CombinePaths(appDataService.DefaultAppDataDirectory, "settings.json");

    public async Task<AppSettings?> LoadSettings()
    {
        if (fileSystem.File.Exists(this.settingsFile))
        {
            logger.LogInformation("Trying to load settings from settings file...");
            return await dataService.LoadAsync<AppSettings>(this.settingsFile);
        }

        return null;
    }

    public AppSettings? LoadSettingsSync()
    {
        if (fileSystem.File.Exists(this.settingsFile))
        {
            logger.LogInformation("Trying to load settings from settings file...");
            return dataService.Load<AppSettings>(this.settingsFile);
        }

        return null;
    }

    public async Task SaveSettings(AppSettings appSettings)
    {
        await dataService.StoreAsync(appSettings, this.settingsFile);
        await UpdateUiThreadCultureAsync(appSettings.SelectedLanguage.CultureInfo);
    }

    public void SaveSettingsSync(AppSettings appSettings)
    {
        dataService.Store(appSettings, this.settingsFile);
    }

    private static async Task UpdateUiThreadCultureAsync(CultureInfo cultureInfo)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        });
    }
}
