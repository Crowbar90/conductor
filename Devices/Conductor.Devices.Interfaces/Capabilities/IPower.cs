using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IPower
{
    Task<PowerState> GetPowerStatus(CancellationToken cancellationToken = default);

    TimeSpan DelayAfterPowerChange { get; }
}