namespace WoWsShipBuilder.Infrastructure.DataTransfer;

#pragma warning disable S2368 // Make this constructor private or simplify to not use multidimensional/jagged arrays
public sealed record GunDataContainer(decimal HPos, decimal VPos, decimal BaseAngle, decimal[] Sector, decimal[][] DeadZones);
#pragma warning restore S2368 // Make this constructor private or simplify to not use multidimensional/jagged arrays
