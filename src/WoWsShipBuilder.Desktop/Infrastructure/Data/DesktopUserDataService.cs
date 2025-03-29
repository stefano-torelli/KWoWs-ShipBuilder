using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DynamicData;
using MudBlazor;
using WoWsShipBuilder.Features.Builds;
using WoWsShipBuilder.Features.Builds.Components;
using WoWsShipBuilder.Infrastructure.ApplicationData;

namespace WoWsShipBuilder.Desktop.Infrastructure.Data;

public class DesktopUserDataService(IDataService dataService, IAppDataService appDataService, IFileSystem fileSystem, IDialogService dialogService) : IUserDataService
{
    private readonly IDataService dataService = dataService;

    private readonly IAppDataService appDataService = appDataService;

    private readonly IFileSystem fileSystem = fileSystem;

    private readonly IDialogService dialogService = dialogService;

    private List<Build>? savedBuilds;

    public async Task SaveBuildsAsync(IEnumerable<Build> builds)
    {
        var path = this.dataService.CombinePaths(this.appDataService.DefaultAppDataDirectory, "builds.json");
        var buildStrings = builds.Select(build => build.CreateShortStringFromBuild()).ToList();
        await this.dataService.StoreAsync(buildStrings, path);
    }

    public async Task<IEnumerable<Build>> LoadBuildsAsync()
    {
        if (this.savedBuilds is not null)
        {
            return this.savedBuilds;
        }

        var path = this.dataService.CombinePaths(this.appDataService.DefaultAppDataDirectory, "builds.json");

        if (!this.fileSystem.File.Exists(path))
        {
            return [];
        }

        List<string>? buildList = null;
        try
        {
            buildList = await this.dataService.LoadAsync<List<string>>(path);
        }
        catch (JsonException)
        {
            // silently fails
        }

        if (buildList is not null)
        {
            var builds = new List<Build>();
            foreach (var buildString in buildList)
            {
                try
                {
                    var build = Build.CreateBuildFromString(buildString);
                    if (AppData.ShipDictionary.ContainsKey(build.ShipIndex))
                    {
                        builds.Add(build);
                    }
                }
                catch (FormatException)
                {
                    // silently fails
                }
            }

            this.savedBuilds = [.. builds.DistinctBy(x => x.Hash)];
        }

        return this.savedBuilds ?? Enumerable.Empty<Build>();
    }

    public async Task ImportBuildsAsync(IEnumerable<Build> builds)
    {
        this.savedBuilds ??= [.. await this.LoadBuildsAsync()];

        var buildsList = builds.ToList();

        foreach (var build in buildsList.Where(x => AppData.ShipDictionary.ContainsKey(x.ShipIndex)))
        {
            this.savedBuilds.RemoveAll(x => x.Equals(build));
            var buildToUpdate = this.savedBuilds.Find(x => x.ShipIndex.Equals(build.ShipIndex, StringComparison.OrdinalIgnoreCase) && x.BuildName.Equals(build.BuildName, StringComparison.OrdinalIgnoreCase));
            if (buildToUpdate != null)
            {
                var updateBuild = await this.OpenUpdateBuildConfirmationDialog(buildToUpdate);
                if (updateBuild)
                {
                    var index = this.savedBuilds.IndexOf(buildToUpdate);
                    this.savedBuilds.Remove(buildToUpdate);
                    this.savedBuilds.Insert(index, build);
                }
            }
            else
            {
                this.savedBuilds.Insert(0, build);
            }
        }

        await this.SaveBuildsAsync(this.savedBuilds);
    }

    public async Task SaveBuildAsync(Build build) => await this.ImportBuildsAsync([build]);

    public async Task RemoveSavedBuildAsync(Build build) => await this.RemoveSavedBuildsAsync([build]);

    public async Task RemoveSavedBuildsAsync(IEnumerable<Build> builds)
    {
        this.savedBuilds ??= [.. await this.LoadBuildsAsync()];
        this.savedBuilds.RemoveMany(builds);

        await this.SaveBuildsAsync(this.savedBuilds);
    }

    private async Task<bool> OpenUpdateBuildConfirmationDialog(Build build)
    {
        DialogOptions options = new()
        {
            NoHeader = true,
            CloseOnEscapeKey = true,
        };
        DialogParameters parameters = new()
        {
            ["BuildName"] = build.BuildName,
            ["ShipIndex"] = build.ShipIndex,
        };
        var result = await (await this.dialogService.ShowAsync<OverwriteExistingBuildConfirmationDialog>(string.Empty, parameters, options)).Result;
        return result is not null && !result.Canceled && (bool)(result.Data ?? false);
    }
}
