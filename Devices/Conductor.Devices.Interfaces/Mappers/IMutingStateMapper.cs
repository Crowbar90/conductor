using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Mappers;

public interface IMutingStateMapper<TMutingState>
    where TMutingState : struct, IConvertible
{
    public MutingState ToCommonMutingState(TMutingState deviceMutingState);
    public TMutingState FromCommonMutingState(MutingState commonMutingState);
}