using Conductor.Scenes.Enums;
using Crowbar90.Common.Utilities;
using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class MutingStateMappingExtensions
{
    private const string MutingCommandPrefix = "MU";

    private static readonly TwoWayDictionary<MutingState, Command> MutingStateMapping = new()
    {
        { MutingState.Unmuted, PowerCommand.Parse($"{MutingCommandPrefix}OFF") },
        { MutingState.Muted, PowerCommand.Parse($"{MutingCommandPrefix}ON") }
    };

    internal static MutingState ToCommonMutingState(this Command deviceMutingState) =>
        MutingStateMapping.TryGetValue(deviceMutingState, out var commonMutingState)
            ? commonMutingState
            : throw new ArgumentOutOfRangeException(nameof(deviceMutingState), deviceMutingState, null);

    internal static Command ToDeviceMutingState(this MutingState commonMutingState) =>
        MutingStateMapping.TryGetValue(commonMutingState, out var deviceMutingState)
            ? deviceMutingState
            : throw new ArgumentOutOfRangeException(nameof(commonMutingState), commonMutingState, null);
}