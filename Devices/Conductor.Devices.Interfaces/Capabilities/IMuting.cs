using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IMuting
{
    Task<MutingState> GetMutingStatus(CancellationToken cancellationToken = default);

    Task<MutingState> Mute(CancellationToken cancellationToken = default);
    Task<MutingState> Unmute(CancellationToken cancellationToken = default);
    Task<MutingState> SwitchMuting(MutingState status, CancellationToken cancellationToken = default);
    Task<MutingState> MutingToggleAsync(CancellationToken cancellationToken = default);

    TimeSpan DelayAfterMutingChange { get; }
}