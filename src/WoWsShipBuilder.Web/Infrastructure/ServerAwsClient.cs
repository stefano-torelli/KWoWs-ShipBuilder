using Microsoft.Extensions.Options;
using WoWsShipBuilder.DataStructures.Versioning;
using WoWsShipBuilder.Infrastructure.ApplicationData;
using WoWsShipBuilder.Infrastructure.GameData;
using WoWsShipBuilder.Infrastructure.HttpClients;

namespace WoWsShipBuilder.Web.Infrastructure;

public class ServerAwsClient(HttpClient httpClient, IOptions<CdnOptions> options, ILogger<ServerAwsClient> logger) : IAwsClient
{
    private readonly ILogger logger = logger;

    private readonly HttpClient httpClient = httpClient;

    private readonly CdnOptions options = options.Value;

    public async Task<VersionInfo> DownloadVersionInfo(ServerType serverType)
    {
        var url = @$"{this.options.Host}/api/{serverType.StringName()}/VersionInfo.json";
        return await this.httpClient.GetFromJsonAsync<VersionInfo>(url, AppConstants.JsonSerializerOptions) ?? throw new HttpRequestException("Unable to process VersionInfo response from AWS server.");
    }

    public async Task DownloadFiles(ServerType serverType, List<(string, string)> relativeFilePaths, IProgress<int>? downloadProgress = null)
    {
        var baseUrl = @$"{this.options.Host}/api/{serverType.StringName()}/";
        var taskList = new List<Task>();
        var totalFiles = relativeFilePaths.Count;
        var finished = 0;
        IProgress<int> progress = new Progress<int>(update =>
        {
            finished += update;
            downloadProgress?.Report(finished / totalFiles);
        });

        foreach ((var category, var fileName) in relativeFilePaths)
        {
            Uri uri = string.IsNullOrWhiteSpace(category) ? new(baseUrl + fileName) : new(baseUrl + $"{category}/{fileName}");
            var task = Task.Run(async () =>
            {
                try
                {
                    await this.DownloadFileAsync(uri, category, fileName);
                }
                catch (HttpRequestException e)
                {
                    this.logger.LogWarning(e, "Encountered an exception while downloading a file with uri {Uri}", uri);
                }

                progress.Report(1);
            });

            taskList.Add(task);
        }

        // Handle exceptions all at one place
        await Task.WhenAll(taskList);
    }

    public async Task<Dictionary<string, string>> DownloadLocalization(string language, ServerType serverType)
    {
        var baseUrl = @$"{this.options.Host}/api/{serverType.StringName()}/";
        return await this.httpClient.GetFromJsonAsync<Dictionary<string, string>>($"{baseUrl}Localization/{language}.json") ?? throw new InvalidOperationException();
    }

    private async Task DownloadFileAsync(Uri uri, string category, string fileName)
    {
        var str = await this.httpClient.GetStringAsync(uri);
        await DataCacheHelper.AddToCache(fileName, category, str);
    }
}
