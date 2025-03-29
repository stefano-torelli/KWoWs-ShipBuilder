using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WoWsShipBuilder.DataStructures.Versioning;
using WoWsShipBuilder.Infrastructure.ApplicationData;
using WoWsShipBuilder.Infrastructure.GameData;
using WoWsShipBuilder.Infrastructure.HttpClients;
using WoWsShipBuilder.Infrastructure.Utility;

namespace WoWsShipBuilder.Web.Infrastructure.Data;

public class ServerAppDataService(IAwsClient awsClient, IOptions<CdnOptions> options, ILogger<ServerAppDataService> logger) : IAppDataService
{
    private readonly CdnOptions options = options.Value;
    private VersionInfo? versionInfo;

    public string DefaultAppDataDirectory { get; } = string.Empty;

    public string AppDataDirectory { get; } = string.Empty;

    public string AppDataImageDirectory { get; } = string.Empty;

    public async Task FetchData()
    {
        logger.LogInformation("Starting to fetch data with server type {Server}...", this.options.Server);
        const string undefinedMarker = "undefined";
        AppData.ResetCaches();

        var onlineVersionInfo = await awsClient.DownloadVersionInfo(this.options.Server);
        if (onlineVersionInfo.CurrentVersion is not null) // check for null for legacy compatibility
        {
            AppData.DataVersion = Helpers.ComputeFullVersionString(onlineVersionInfo);
            logger.LogInformation("Found online version info with version {Version}", AppData.DataVersion);
        }
        else
        {
            AppData.DataVersion = undefinedMarker;
            logger.LogWarning("Online version info not available");
        }

        SentrySdk.ConfigureScope(scope =>
        {
            var mainVersionString = undefinedMarker;
            if (onlineVersionInfo.CurrentVersion?.MainVersion is { } mainVersion)
            {
                mainVersionString = Helpers.ComputeMainVersionString(mainVersion);
            }
            scope.SetTag("data.version", mainVersionString);
            scope.SetTag("data.iteration", onlineVersionInfo.CurrentVersion?.DataIteration.ToString(CultureInfo.InvariantCulture) ?? undefinedMarker);
            scope.SetTag("data.server", onlineVersionInfo.CurrentVersion?.VersionType.ToString() ?? undefinedMarker);
        });
        var files = onlineVersionInfo.Categories.SelectMany(category => category.Value.Select(file => (category.Key, file.FileName))).ToList();
        await awsClient.DownloadFiles(this.options.Server, files);
        Helpers.InitializeShipSelectorDataStructure();
        logger.LogInformation("Finished fetching data");
    }

    public async Task LoadLocalFilesAsync(ServerType serverType)
    {
        AppData.ResetCaches();
        const string shipBuilderDirectory = "WoWsShipBuilderDev";
        var dataRoot = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), shipBuilderDirectory, "json", serverType.StringName());

        var versionInfoContent = await File.ReadAllTextAsync(Path.Join(dataRoot, "VersionInfo.json"));
        var localVersionInfo = JsonSerializer.Deserialize<VersionInfo>(versionInfoContent, AppConstants.JsonSerializerOptions)!;

        AppData.DataVersion = Helpers.ComputeFullVersionString(localVersionInfo);

        var dataRootInfo = new DirectoryInfo(dataRoot);
        var categories = dataRootInfo.GetDirectories();
        foreach (var category in categories)
        {
            if (category.Name.Contains("Localization", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }
            foreach (var file in category.GetFiles())
            {
                var content = await File.ReadAllTextAsync(file.FullName);
                await DataCacheHelper.AddToCache(file.Name, category.Name, content);
            }
        }

        Helpers.InitializeShipSelectorDataStructure();
    }

    public async Task<VersionInfo?> GetCurrentVersionInfo(ServerType serverType)
    {
        this.versionInfo ??= await awsClient.DownloadVersionInfo(serverType);
        return this.versionInfo;
    }

    public async Task<Dictionary<string, string>?> ReadLocalizationData(ServerType serverType, string language)
    {
        if (this.options.UseLocalFiles)
        {
            const string shipBuilderDirectory = "WoWsShipBuilderDev";
            var localizationRoot = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), shipBuilderDirectory, "json", this.options.Server.StringName(), "Localization");
            var file = Path.Join(localizationRoot, $"{language}.json");
            if (!File.Exists(file))
            {
                return [];
            }

            var fileContent = await File.ReadAllTextAsync(Path.Join(localizationRoot, $"{language}.json"));
            return JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent, AppConstants.JsonSerializerOptions);
        }

        if (awsClient is ServerAwsClient serverAwsClient)
        {
            return await serverAwsClient.DownloadLocalization(language, serverType);
        }

        return null;
    }

    public string GetDataPath(ServerType serverType)
    {
        throw new InvalidOperationException();
    }

    public string GetLocalizationPath(ServerType serverType) => string.Empty;

    public Task<List<string>> GetInstalledLocales(ServerType serverType, bool includeFileType = true)
    {
        return Task.FromResult(AppConstants.SupportedLanguages.Select(language => language.LocalizationFileName).ToList());
    }
}
