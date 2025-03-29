namespace WoWsShipBuilder.Features.ShipComparison.GridData;

public class PlaneGridDataWrapper
{
    public NoSortList<string> Type { get; protected init; } = [];

    public NoSortList<int> InSquadron { get; protected init; } = [];

    public NoSortList<int> PerAttack { get; protected init; } = [];

    public NoSortList<int> OnDeck { get; protected init; } = [];

    public NoSortList<decimal> RestorationTime { get; protected init; } = [];

    public NoSortList<decimal> CruisingSpeed { get; protected init; } = [];

    public NoSortList<decimal> MaxSpeed { get; protected init; } = [];

    public NoSortList<decimal> MinSpeed { get; protected init; } = [];

    public NoSortList<decimal> EngineBoostDuration { get; protected init; } = [];

    public NoSortList<decimal> InitialBoostDuration { get; protected init; } = [];

    public NoSortList<decimal> InitialBoostValue { get; protected init; } = [];

    public NoSortList<int> PlaneHp { get; protected init; } = [];

    public NoSortList<int> SquadronHp { get; protected init; } = [];

    public NoSortList<int> AttackGroupHp { get; protected init; } = [];

    public NoSortList<int> DamageDuringAttack { get; protected init; } = [];

    public NoSortList<int> WeaponsPerPlane { get; protected init; } = [];

    public NoSortList<decimal> PreparationTime { get; protected init; } = [];

    public NoSortList<decimal> AimingTime { get; protected init; } = [];

    public NoSortList<decimal> TimeToFullyAimed { get; protected init; } = [];

    public NoSortList<decimal> PostAttackInvulnerability { get; protected init; } = [];

    public NoSortList<decimal> AttackCooldown { get; protected init; } = [];

    public NoSortList<decimal> Concealment { get; protected init; } = [];

    public NoSortList<decimal> Spotting { get; protected init; } = [];

    public NoSortList<string> AreaChangeAiming { get; protected init; } = [];

    public NoSortList<string> AreaChangePreparation { get; protected init; } = [];
}
