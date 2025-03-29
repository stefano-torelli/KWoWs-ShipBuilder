using WoWsShipBuilder.DataStructures.Versioning;
using WoWsShipBuilder.Infrastructure.GameData;

namespace WoWsShipBuilder.Infrastructure.HttpClients;

public interface IAwsClient
{
    Task<VersionInfo> DownloadVersionInfo(ServerType serverType);

    Task DownloadFiles(ServerType serverType, List<(string, string)> relativeFilePaths, IProgress<int>? downloadProgress = null);
}
