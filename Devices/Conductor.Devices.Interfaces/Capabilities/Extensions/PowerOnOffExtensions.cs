using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities.Extensions;

public static class PowerOnOffExtensions
{
    public static Task<PowerState> PowerOn(this IPowerOnOff device, CancellationToken cancellationToken = default) =>
        device.SwitchPower(PowerState.On, cancellationToken);
    
    public static Task<PowerState> PowerOff(this IPowerOnOff device, CancellationToken cancellationToken = default) =>
        device.SwitchPower(PowerState.Off, cancellationToken);
    
    public static async Task<PowerState> PowerToggle(this IPowerOnOff device, CancellationToken cancellationToken = default) => 
        await device.SwitchPower((await device.GetPowerStatus(cancellationToken)).Opposite(), cancellationToken);
}