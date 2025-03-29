namespace WoWsShipBuilder.Web.Features.BetaAccess;

public interface IBetaAccessManager
{
    IEnumerable<BetaAccessEntry> ActiveBetas { get; }

    BetaAccessEntry? FindBetaByCode(string code);

    bool IsBetaActive(BetaAccessEntry entry);
}
