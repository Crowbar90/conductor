using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IPowerOnOff : IPower
{
    Task<PowerState> SwitchPower(PowerState status, CancellationToken cancellationToken = default);
}