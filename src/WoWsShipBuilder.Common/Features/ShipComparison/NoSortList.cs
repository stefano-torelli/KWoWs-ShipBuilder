namespace WoWsShipBuilder.Features.ShipComparison;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1036 // Override methods on comparable types
#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
public class NoSortList<T> : List<T>, IComparable
#pragma warning restore CA1036
{
    public NoSortList()
    {
    }

    public NoSortList(IEnumerable<T> collection)
        : base(collection)
    {
    }

    public int CompareTo(object? obj)
    {
        return 0;
    }
#pragma warning restore S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
#pragma warning restore CA1036 // Override methods on comparable types
#pragma warning restore IDE0079 // Remove unnecessary suppression
}
