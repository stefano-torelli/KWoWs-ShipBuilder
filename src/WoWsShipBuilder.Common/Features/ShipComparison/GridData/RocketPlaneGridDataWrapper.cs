using WoWsShipBuilder.DataStructures;
using WoWsShipBuilder.Features.DataContainers;

namespace WoWsShipBuilder.Features.ShipComparison.GridData;

public class RocketPlaneGridDataWrapper : PlaneGridDataWrapper
{
    public RocketPlaneGridDataWrapper(IReadOnlyCollection<CvAircraftDataContainer>? rocketPlanes)
    {
        this.Type = rocketPlanes?.Select(x => x.PlaneVariant).ToNoSortList() ?? [];
        this.InSquadron = rocketPlanes?.Select(x => x.NumberInSquad).ToNoSortList() ?? [];
        this.PerAttack = rocketPlanes?.Select(x => x.NumberDuringAttack).ToNoSortList() ?? [];
        this.OnDeck = rocketPlanes?.Select(x => x.MaxNumberOnDeck).ToNoSortList() ?? [];
        this.RestorationTime = rocketPlanes?.Select(x => x.RestorationTime).ToNoSortList() ?? [];
        this.CruisingSpeed = rocketPlanes?.Select(x => x.CruisingSpeed).ToNoSortList() ?? [];
        this.MaxSpeed = rocketPlanes?.Select(x => x.MaxSpeed).ToNoSortList() ?? [];
        this.MinSpeed = rocketPlanes?.Select(x => x.MinSpeed).ToNoSortList() ?? [];
        this.EngineBoostDuration = rocketPlanes?.Select(x => x.MaxEngineBoostDuration).ToNoSortList() ?? [];
        this.InitialBoostDuration = rocketPlanes?.Select(x => x.JatoDuration).ToNoSortList() ?? [];
        this.InitialBoostValue = rocketPlanes?.Select(x => x.JatoSpeedMultiplier).ToNoSortList() ?? [];
        this.PlaneHp = rocketPlanes?.Select(x => x.PlaneHp).ToNoSortList() ?? [];
        this.SquadronHp = rocketPlanes?.Select(x => x.SquadronHp).ToNoSortList() ?? [];
        this.AttackGroupHp = rocketPlanes?.Select(x => x.AttackGroupHp).ToNoSortList() ?? [];
        this.DamageDuringAttack = rocketPlanes?.Select(x => x.DamageTakenDuringAttack).ToNoSortList() ?? [];
        this.WeaponsPerPlane = rocketPlanes?.Select(x => x.AmmoPerAttack).ToNoSortList() ?? [];
        this.PreparationTime = rocketPlanes?.Select(x => x.PreparationTime).ToNoSortList() ?? [];
        this.AimingTime = rocketPlanes?.Select(x => x.AimingTime).ToNoSortList() ?? [];
        this.TimeToFullyAimed = rocketPlanes?.Select(x => x.TimeToFullyAimed).ToNoSortList() ?? [];
        this.PostAttackInvulnerability = rocketPlanes?.Select(x => x.PostAttackInvulnerabilityDuration).ToNoSortList() ?? [];
        this.AttackCooldown = rocketPlanes?.Select(x => x.AttackCd).ToNoSortList() ?? [];
        this.Concealment = rocketPlanes?.Select(x => x.ConcealmentFromShips).ToNoSortList() ?? [];
        this.Spotting = rocketPlanes?.Select(x => x.MaxViewDistance).ToNoSortList() ?? [];
        this.AreaChangeAiming = rocketPlanes?.Select(x => x.AimingRateMoving).ToNoSortList() ?? [];
        this.AreaChangePreparation = rocketPlanes?.Select(x => x.AimingPreparationRateMoving).ToNoSortList() ?? [];

        // Rockets
        List<RocketDataContainer?>? rockets = rocketPlanes?.Select(x => x.Weapon as RocketDataContainer).ToNoSortList();

        this.WeaponType = rockets?.Select(x => x?.RocketType ?? default!).ToNoSortList() ?? [];
        this.WeaponDamage = rockets?.Select(x => x?.Damage ?? 0).ToNoSortList() ?? [];
        this.WeaponSplashRadius = rockets?.Select(x => x?.SplashRadius ?? 0).ToNoSortList() ?? [];
        this.WeaponSplashDamage = rockets?.Select(x => x?.SplashDmg ?? 0).ToNoSortList() ?? [];
        this.WeaponPenetration = rockets?.Select(x => (x?.RocketType == $"ArmamentType_{RocketType.AP}" ? x.PenetrationAp : x?.Penetration) ?? 0).ToNoSortList() ?? [];
        this.WeaponFireChance = rockets?.Select(x => x?.FireChance ?? 0).ToNoSortList() ?? [];
        this.WeaponFuseTimer = rockets?.Select(x => x?.FuseTimer ?? 0).ToNoSortList() ?? [];
        this.WeaponArmingThreshold = rockets?.Select(x => x?.ArmingThreshold ?? 0).ToNoSortList() ?? [];
        this.WeaponRicochetAngles = rockets?.Select(x => x?.RicochetAngles ?? default!).ToNoSortList() ?? [];
        this.WeaponBlastRadius = rockets?.Select(x => x?.ExplosionRadius ?? 0).ToNoSortList() ?? [];
        this.WeaponBlastPenetration = rockets?.Select(x => x?.SplashCoeff ?? 0).ToNoSortList() ?? [];
    }

    public NoSortList<string> WeaponType { get; }

    public NoSortList<decimal> WeaponDamage { get; }

    public NoSortList<decimal> WeaponSplashRadius { get; }

    public NoSortList<decimal> WeaponSplashDamage { get; }

    public NoSortList<int> WeaponPenetration { get; }

    public NoSortList<decimal> WeaponFireChance { get; }

    public NoSortList<decimal> WeaponFuseTimer { get; }

    public NoSortList<int> WeaponArmingThreshold { get; }

    public NoSortList<string> WeaponRicochetAngles { get; }

    public NoSortList<decimal> WeaponBlastRadius { get; }

    public NoSortList<decimal> WeaponBlastPenetration { get; }
}
