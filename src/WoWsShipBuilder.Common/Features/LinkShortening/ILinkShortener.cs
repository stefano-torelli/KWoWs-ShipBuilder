using WoWsShipBuilder.Features.Builds;

namespace WoWsShipBuilder.Features.LinkShortening;

public interface ILinkShortener
{
    bool IsAvailable { get; }

    Task<ShorteningResult> CreateLinkForBuild(Build build);

    Task<ShorteningResult> CreateShortLink(string link);
}
