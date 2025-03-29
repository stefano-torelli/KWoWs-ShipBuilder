namespace WoWsShipBuilder.Web.Infrastructure;

public class AdminOptions
{
    public const string SectionName = "AdminSettings";

    public string WgApiKey { get; set; } = "";

    public string[] AdminUsers { get; set; } = [];

    public string[] BuildCurators { get; set; } = [];
}
