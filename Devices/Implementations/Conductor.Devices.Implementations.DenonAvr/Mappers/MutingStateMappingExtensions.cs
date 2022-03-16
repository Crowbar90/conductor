using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class MutingStateMappingExtensions
{
    private const string MutingCommandPrefix = "MU";
    
    internal static MutingState ToCommonMutingState(this Command deviceMutingState)
    {
        if (deviceMutingState.Code != MutingCommandPrefix)
            throw new ArgumentOutOfRangeException(nameof(deviceMutingState), deviceMutingState, null);

        return deviceMutingState.Value switch
        {
            "OFF" => MutingState.Unmuted,
            "ON" => MutingState.Muted,
            _ => throw new ArgumentOutOfRangeException(nameof(deviceMutingState), deviceMutingState, null)
        };
    }

    internal static Command ToDeviceMutingState(this MutingState commonMutingState) =>
        commonMutingState switch
        {
            MutingState.Muted => MuteCommand.Parse($"{MutingCommandPrefix}ON"),
            MutingState.Unmuted => MuteCommand.Parse($"{MutingCommandPrefix}OFF"),
            _ => throw new ArgumentOutOfRangeException(nameof(commonMutingState), commonMutingState, null)
        };
}