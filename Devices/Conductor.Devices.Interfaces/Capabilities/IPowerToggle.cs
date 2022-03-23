using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IPowerToggle : IPower
{
    Task<PowerState> PowerToggleAsync(CancellationToken cancellationToken = default);
}