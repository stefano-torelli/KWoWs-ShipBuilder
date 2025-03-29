using WoWsShipBuilder.Features.DataContainers;

namespace WoWsShipBuilder.Features.ShipComparison.GridData;

public class ManeuverabilityGridDataWrapper(ManeuverabilityDataContainer maneuverability)
{
    public decimal MaxSpeed { get; } = maneuverability.ManeuverabilityMaxSpeed != 0 ? maneuverability.ManeuverabilityMaxSpeed : maneuverability.ManeuverabilitySubsMaxSpeedOnSurface;

    public decimal MaxSpeedAtPeriscopeDepth { get; } = maneuverability.ManeuverabilitySubsMaxSpeedAtPeriscope;

    public decimal MaxSpeedAtMaxDepth { get; } = maneuverability.ManeuverabilitySubsMaxSpeedAtMaxDepth;

    public decimal MaxReverseSpeed { get; } = maneuverability.ManeuverabilityMaxReverseSpeed != 0 ? maneuverability.ManeuverabilityMaxReverseSpeed : maneuverability.ManeuverabilitySubsMaxReverseSpeedOnSurface;

    public decimal MaxReverseSpeedAtPeriscopeDepth { get; } = maneuverability.ManeuverabilitySubsMaxReverseSpeedAtPeriscope;

    public decimal MaxReverseSpeedAtMaxDepth { get; } = maneuverability.ManeuverabilitySubsMaxReverseSpeedAtMaxDepth;

    public decimal MaxDiveSpeed { get; } = maneuverability.ManeuverabilitySubsMaxDiveSpeed;

    public decimal DivingPlaneShiftTime { get; } = maneuverability.ManeuverabilitySubsDivingPlaneShiftTime;

    public decimal RudderShiftTime { get; } = maneuverability.ManeuverabilityRudderShiftTime;

    public decimal TurningCircle { get; } = maneuverability.ManeuverabilityTurningCircle;

    public decimal TimeToFullAhead { get; } = maneuverability.ForwardMaxSpeedTime;

    public decimal TimeToFullReverse { get; } = maneuverability.ReverseMaxSpeedTime;

    public decimal RudderProtection { get; } = maneuverability.RudderBlastProtection;

    public decimal EngineProtection { get; } = maneuverability.EngineBlastProtection;
}
