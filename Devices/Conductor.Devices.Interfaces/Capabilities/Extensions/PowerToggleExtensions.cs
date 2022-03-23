using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities.Extensions;

public static class PowerToggleExtensions
{
    public static Task<PowerState> PowerOn(this IPowerToggle device, CancellationToken cancellationToken = default) =>
        device.SwitchPower(PowerState.On, cancellationToken);
    
    public static Task<PowerState> PowerOff(this IPowerToggle device, CancellationToken cancellationToken = default) =>
        device.SwitchPower(PowerState.Off, cancellationToken);

    public static async Task<PowerState> SwitchPower(
        this IPowerToggle device,
        PowerState status,
        CancellationToken cancellationToken = default) =>
        await device.GetPowerStatus(cancellationToken) == status
            ? status
            : await device.PowerToggleAsync(cancellationToken);
}