using WoWsShipBuilder.Features.Settings;

namespace WoWsShipBuilder.Infrastructure.ApplicationData;

public interface ISettingsAccessor
{
    Task<AppSettings?> LoadSettings();

    Task SaveSettings(AppSettings appSettings);
}
