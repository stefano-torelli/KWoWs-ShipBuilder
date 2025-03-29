using System.Collections.Immutable;
using WoWsShipBuilder.DataStructures;
using WoWsShipBuilder.Features.DataContainers;

namespace WoWsShipBuilder.Features.ShipComparison.GridData;

public class TorpedoBomberGridDataWrapper : PlaneGridDataWrapper
{
    public TorpedoBomberGridDataWrapper(IReadOnlyCollection<CvAircraftDataContainer>? torpedoBombers)
    {
        this.Type = torpedoBombers?.Select(x => x.PlaneVariant).ToNoSortList() ?? [];
        this.InSquadron = torpedoBombers?.Select(x => x.NumberInSquad).ToNoSortList() ?? [];
        this.PerAttack = torpedoBombers?.Select(x => x.NumberDuringAttack).ToNoSortList() ?? [];
        this.OnDeck = torpedoBombers?.Select(x => x.MaxNumberOnDeck).ToNoSortList() ?? [];
        this.RestorationTime = torpedoBombers?.Select(x => x.RestorationTime).ToNoSortList() ?? [];
        this.CruisingSpeed = torpedoBombers?.Select(x => x.CruisingSpeed).ToNoSortList() ?? [];
        this.MaxSpeed = torpedoBombers?.Select(x => x.MaxSpeed).ToNoSortList() ?? [];
        this.MinSpeed = torpedoBombers?.Select(x => x.MinSpeed).ToNoSortList() ?? [];
        this.EngineBoostDuration = torpedoBombers?.Select(x => x.MaxEngineBoostDuration).ToNoSortList() ?? [];
        this.InitialBoostDuration = torpedoBombers?.Select(x => x.JatoDuration).ToNoSortList() ?? [];
        this.InitialBoostValue = torpedoBombers?.Select(x => x.JatoSpeedMultiplier).ToNoSortList() ?? [];
        this.PlaneHp = torpedoBombers?.Select(x => x.PlaneHp).ToNoSortList() ?? [];
        this.SquadronHp = torpedoBombers?.Select(x => x.SquadronHp).ToNoSortList() ?? [];
        this.AttackGroupHp = torpedoBombers?.Select(x => x.AttackGroupHp).ToNoSortList() ?? [];
        this.DamageDuringAttack = torpedoBombers?.Select(x => x.DamageTakenDuringAttack).ToNoSortList() ?? [];
        this.WeaponsPerPlane = torpedoBombers?.Select(x => x.AmmoPerAttack).ToNoSortList() ?? [];
        this.PreparationTime = torpedoBombers?.Select(x => x.PreparationTime).ToNoSortList() ?? [];
        this.AimingTime = torpedoBombers?.Select(x => x.AimingTime).ToNoSortList() ?? [];
        this.TimeToFullyAimed = torpedoBombers?.Select(x => x.TimeToFullyAimed).ToNoSortList() ?? [];
        this.PostAttackInvulnerability = torpedoBombers?.Select(x => x.PostAttackInvulnerabilityDuration).ToNoSortList() ?? [];
        this.AttackCooldown = torpedoBombers?.Select(x => x.AttackCd).ToNoSortList() ?? [];
        this.Concealment = torpedoBombers?.Select(x => x.ConcealmentFromShips).ToNoSortList() ?? [];
        this.Spotting = torpedoBombers?.Select(x => x.MaxViewDistance).ToNoSortList() ?? [];
        this.AreaChangeAiming = torpedoBombers?.Select(x => x.AimingRateMoving).ToNoSortList() ?? [];
        this.AreaChangePreparation = torpedoBombers?.Select(x => x.AimingPreparationRateMoving).ToNoSortList() ?? [];

        List<TorpedoDataContainer?>? aerialTorpedoes = torpedoBombers?.Select(x => x.Weapon as TorpedoDataContainer).ToNoSortList();

        this.WeaponType = aerialTorpedoes?.Select(x => x?.TorpedoType ?? default!).ToNoSortList() ?? [];
        this.WeaponDamage = aerialTorpedoes?.Select(x => x?.Damage ?? 0).ToNoSortList() ?? [];
        this.WeaponRange = aerialTorpedoes?.Select(x => x?.Range ?? 0).ToNoSortList() ?? [];
        this.WeaponSpeed = aerialTorpedoes?.Select(x => x?.Speed ?? 0).ToNoSortList() ?? [];
        this.WeaponDetectabilityRange = aerialTorpedoes?.Select(x => x?.Detectability ?? 0).ToNoSortList() ?? [];
        this.WeaponArmingDistance = aerialTorpedoes?.Select(x => x?.ArmingDistance ?? 0).ToNoSortList() ?? [];
        this.WeaponReactionTime = aerialTorpedoes?.Select(x => x?.ReactionTime ?? 0).ToNoSortList() ?? [];
        this.WeaponFloodingChance = aerialTorpedoes?.Select(x => x?.FloodingChance ?? 0).ToNoSortList() ?? [];
        this.WeaponBlastRadius = aerialTorpedoes?.Select(x => x?.ExplosionRadius ?? 0).ToNoSortList() ?? [];
        this.WeaponBlastPenetration = aerialTorpedoes?.Select(x => x?.SplashCoeff ?? 0).ToNoSortList() ?? [];
        this.WeaponCanHit = aerialTorpedoes?.Select(x => x?.CanHitClasses ?? []).ToNoSortList() ?? [];
    }

    public NoSortList<string> WeaponType { get; }

    public NoSortList<decimal> WeaponDamage { get; }

    public NoSortList<decimal> WeaponRange { get; }

    public NoSortList<decimal> WeaponSpeed { get; }

    public NoSortList<decimal> WeaponDetectabilityRange { get; }

    public NoSortList<int> WeaponArmingDistance { get; }

    public NoSortList<decimal> WeaponReactionTime { get; }

    public NoSortList<decimal> WeaponFloodingChance { get; }

    public NoSortList<decimal> WeaponBlastRadius { get; }

    public NoSortList<decimal> WeaponBlastPenetration { get; }

    public NoSortList<ImmutableList<ShipClass>> WeaponCanHit { get; }
}
