using WoWsShipBuilder.DataStructures;
using WoWsShipBuilder.Features.DataContainers;

namespace WoWsShipBuilder.Features.ShipComparison.GridData;

public class BomberGridDataWrapper : PlaneGridDataWrapper
{
    public BomberGridDataWrapper(IReadOnlyCollection<CvAircraftDataContainer>? bombers)
    {
        this.Type = bombers?.Select(x => x.PlaneVariant).ToNoSortList() ?? [];
        this.InSquadron = bombers?.Select(x => x.NumberInSquad).ToNoSortList() ?? [];
        this.PerAttack = bombers?.Select(x => x.NumberDuringAttack).ToNoSortList() ?? [];
        this.OnDeck = bombers?.Select(x => x.MaxNumberOnDeck).ToNoSortList() ?? [];
        this.RestorationTime = bombers?.Select(x => x.RestorationTime).ToNoSortList() ?? [];
        this.CruisingSpeed = bombers?.Select(x => x.CruisingSpeed).ToNoSortList() ?? [];
        this.MaxSpeed = bombers?.Select(x => x.MaxSpeed).ToNoSortList() ?? [];
        this.MinSpeed = bombers?.Select(x => x.MinSpeed).ToNoSortList() ?? [];
        this.EngineBoostDuration = bombers?.Select(x => x.MaxEngineBoostDuration).ToNoSortList() ?? [];
        this.InitialBoostDuration = bombers?.Select(x => x.JatoDuration).ToNoSortList() ?? [];
        this.InitialBoostValue = bombers?.Select(x => x.JatoSpeedMultiplier).ToNoSortList() ?? [];
        this.PlaneHp = bombers?.Select(x => x.PlaneHp).ToNoSortList() ?? [];
        this.SquadronHp = bombers?.Select(x => x.SquadronHp).ToNoSortList() ?? [];
        this.AttackGroupHp = bombers?.Select(x => x.AttackGroupHp).ToNoSortList() ?? [];
        this.DamageDuringAttack = bombers?.Select(x => x.DamageTakenDuringAttack).ToNoSortList() ?? [];
        this.WeaponsPerPlane = bombers?.Select(x => x.AmmoPerAttack).ToNoSortList() ?? [];
        this.PreparationTime = bombers?.Select(x => x.PreparationTime).ToNoSortList() ?? [];
        this.AimingTime = bombers?.Select(x => x.AimingTime).ToNoSortList() ?? [];
        this.TimeToFullyAimed = bombers?.Select(x => x.TimeToFullyAimed).ToNoSortList() ?? [];
        this.PostAttackInvulnerability = bombers?.Select(x => x.PostAttackInvulnerabilityDuration).ToNoSortList() ?? [];
        this.AttackCooldown = bombers?.Select(x => x.AttackCd).ToNoSortList() ?? [];
        this.Concealment = bombers?.Select(x => x.ConcealmentFromShips).ToNoSortList() ?? [];
        this.Spotting = bombers?.Select(x => x.MaxViewDistance).ToNoSortList() ?? [];
        this.AreaChangeAiming = bombers?.Select(x => x.AimingRateMoving).ToNoSortList() ?? [];
        this.AreaChangePreparation = bombers?.Select(x => x.AimingPreparationRateMoving).ToNoSortList() ?? [];
        this.InnerEllipse = bombers?.Select(x => x.InnerBombPercentage).ToNoSortList() ?? [];

        List<BombDataContainer?>? bombs = bombers?.Select(x => x.Weapon as BombDataContainer).ToNoSortList();

        this.WeaponType = bombers?.Select(x => x.WeaponType).ToNoSortList() ?? [];
        this.WeaponBombType = bombs?.Select(x => x?.BombType ?? default!).ToNoSortList() ?? [];
        this.WeaponDamage = bombs?.Select(x => x?.Damage ?? 0).ToNoSortList() ?? [];
        this.WeaponSplashRadius = bombs?.Select(x => x?.SplashRadius ?? 0).ToNoSortList() ?? [];
        this.WeaponSplashDamage = bombs?.Select(x => x?.SplashDmg ?? 0).ToNoSortList() ?? [];
        this.WeaponPenetration = bombs?.Select(x => (x?.BombType == $"ArmamentType_{BombType.AP}" ? x.PenetrationAp : x?.Penetration) ?? 0).ToNoSortList() ?? [];
        this.WeaponFireChance = bombs?.Select(x => x?.FireChance ?? 0).ToNoSortList() ?? [];
        this.WeaponBlastRadius = bombs?.Select(x => x?.ExplosionRadius ?? 0).ToNoSortList() ?? [];
        this.WeaponBlastPenetration = bombs?.Select(x => x?.SplashCoeff ?? 0).ToNoSortList() ?? [];
        this.WeaponFuseTimer = bombs?.Select(x => x?.FuseTimer ?? 0).ToNoSortList() ?? [];
        this.WeaponArmingThreshold = bombs?.Select(x => x?.ArmingThreshold ?? 0).ToNoSortList() ?? [];
        this.WeaponRicochetAngles = bombs?.Select(x => x?.RicochetAngles ?? default!).ToNoSortList() ?? [];
    }

    public NoSortList<int> InnerEllipse { get; }

    public NoSortList<string> WeaponType { get; }

    public NoSortList<string> WeaponBombType { get; }

    public NoSortList<decimal> WeaponDamage { get; }

    public NoSortList<decimal> WeaponSplashRadius { get; }

    public NoSortList<decimal> WeaponSplashDamage { get; }

    public NoSortList<int> WeaponPenetration { get; }

    public NoSortList<decimal> WeaponFireChance { get; }

    public NoSortList<decimal> WeaponBlastRadius { get; }

    public NoSortList<decimal> WeaponBlastPenetration { get; }

    public NoSortList<decimal> WeaponFuseTimer { get; }

    public NoSortList<int> WeaponArmingThreshold { get; }

    public NoSortList<string> WeaponRicochetAngles { get; }
}
