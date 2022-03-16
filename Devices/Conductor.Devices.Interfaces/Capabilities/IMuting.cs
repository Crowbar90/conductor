using Conductor.Devices.Interfaces.Mappers;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IMuting
{ }

public interface IMuting<TMutingState> : IMuting
    where TMutingState : struct, IConvertible
{
    Type AudioModeMapperType => typeof(IMutingStateMapper<TMutingState>);
    
    IDictionary<TMutingState, string> MutingStatuses { get; }

    Task<TMutingState?> GetMutingStatus(CancellationToken cancellationToken = default);

    Task<TMutingState?> Mute(CancellationToken cancellationToken = default);
    Task<TMutingState?> Unmute(CancellationToken cancellationToken = default);
    Task<TMutingState?> SwitchMuting(TMutingState status, CancellationToken cancellationToken = default);
    Task<TMutingState?> MutingToggleAsync(CancellationToken cancellationToken = default);
}