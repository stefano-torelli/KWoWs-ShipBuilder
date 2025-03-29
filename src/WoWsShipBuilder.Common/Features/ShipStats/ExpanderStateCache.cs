using System.Collections.Concurrent;
using WoWsShipBuilder.Features.Settings;

namespace WoWsShipBuilder.Features.ShipStats;

public sealed class ExpanderStateCache(AppSettings appSettings)
{
    private readonly AppSettings appSettings = appSettings;

    private readonly ConcurrentDictionary<string, bool> expanderStates = new();

    public bool this[string key]
    {
        get => this.expanderStates.GetOrAdd(key, this.ComputeInitialState);
        set => this.expanderStates[key] = value;
    }

    public void Reset() => this.expanderStates.Clear();

    private bool ComputeInitialState(string key)
    {
        if (key.StartsWith("main", StringComparison.OrdinalIgnoreCase))
        {
            return this.appSettings.OpenAllMainExpandersByDefault;
        }

        if (key.StartsWith("ammo", StringComparison.OrdinalIgnoreCase))
        {
            return this.appSettings.OpenAllAmmoExpandersByDefault;
        }

        if (key.StartsWith("sec", StringComparison.OrdinalIgnoreCase))
        {
            return this.appSettings.OpenSecondariesAndAaExpandersByDefault;
        }

        return false;
    }
}
