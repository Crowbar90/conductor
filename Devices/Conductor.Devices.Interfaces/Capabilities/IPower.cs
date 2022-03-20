using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IPower
{
    Task<PowerState> GetPowerStatus(CancellationToken cancellationToken = default);

    Task<PowerState> PowerOn(CancellationToken cancellationToken = default);
    Task<PowerState> PowerOff(CancellationToken cancellationToken = default);
    Task<PowerState> SwitchPower(PowerState status, CancellationToken cancellationToken = default);
    Task<PowerState> PowerToggleAsync(CancellationToken cancellationToken = default);

    TimeSpan DelayAfterPowerChange { get; }
}