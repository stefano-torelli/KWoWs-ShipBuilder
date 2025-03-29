using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ReactiveUI;
using WoWsShipBuilder.Desktop.Features.Updater;
using WoWsShipBuilder.Features.Settings;
using WoWsShipBuilder.Infrastructure.ApplicationData;
using WoWsShipBuilder.Infrastructure.Localization;
using WoWsShipBuilder.Infrastructure.Localization.Resources;
using WoWsShipBuilder.Infrastructure.Utility;

namespace WoWsShipBuilder.Desktop.Features.SplashScreen;

public partial class SplashScreenViewModel(ILocalDataUpdater localDataUpdater, ILocalizationProvider localizationProvider, IAppDataService appDataService, AppSettings appSettings, ILogger<SplashScreenViewModel> logger) : ReactiveObject
{
    private const int TaskNumber = 4;

    private readonly ILocalDataUpdater localDataUpdater = localDataUpdater;

    private readonly ILocalizationProvider localizationProvider = localizationProvider;

    private readonly IAppDataService appDataService = appDataService;

    private readonly AppSettings appSettings = appSettings;

    private readonly ILogger<SplashScreenViewModel> logger = logger;

    [Observable]
    private int progress;

    [Observable]
    private string downloadInfo = nameof(Translation.SplashScreen_Init);

    public SplashScreenViewModel()
        : this(null!, null!, null!, new(), NullLogger<SplashScreenViewModel>.Instance)
    {
    }

    public async Task VersionCheck(bool forceVersionCheck = false, bool throwOnException = false)
    {
        this.logger.LogDebug("Checking gamedata versions...");
        IProgress<(int, string)> progressTracker = new Progress<(int state, string title)>(value =>
        {
            // ReSharper disable once PossibleLossOfFraction
            this.Progress = value.state * 100 / TaskNumber;
            this.DownloadInfo = value.title;
        });

        try
        {
            await this.localDataUpdater.RunDataUpdateCheck(this.appSettings.SelectedServerType, progressTracker, forceVersionCheck);
            await this.localizationProvider.RefreshDataAsync(this.appSettings.SelectedServerType, this.appSettings.SelectedLanguage);
            await this.appDataService.LoadLocalFilesAsync(this.appSettings.SelectedServerType);
            this.logger.LogDebug("Version check and update tasks completed. Launching main window");
        }
        catch (Exception e)
        {
            if (throwOnException)
            {
                this.logger.LogWarning(e, "Encountered unexpected exception during version check");
                throw;
            }

            this.logger.LogError(e, "Encountered unexpected exception during version check");
        }

        progressTracker.Report((TaskNumber, nameof(Translation.SplashScreen_Done)));
    }
}
