using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class MutingStateMappingExtensions
{
    private const string MutingCommandPrefix = "MU";

    private static readonly TwoWayDictionary<string, Command> MutingStateMapping = new()
    {
        { Resources.ResourceKeys.MutingState.Off, PowerCommand.Parse($"{MutingCommandPrefix}OFF") },
        { Resources.ResourceKeys.MutingState.On, PowerCommand.Parse($"{MutingCommandPrefix}ON") }
    };

    internal static string ToCommonMutingState(this Command deviceMutingState) =>
        MutingStateMapping.TryGetValue(deviceMutingState, out var commonMutingState)
            ? commonMutingState
            : throw new ArgumentOutOfRangeException(nameof(deviceMutingState), deviceMutingState, null);

    internal static Command ToDeviceMutingState(this string commonMutingState) =>
        MutingStateMapping.TryGetValue(commonMutingState, out var deviceMutingState)
            ? deviceMutingState
            : throw new ArgumentOutOfRangeException(nameof(commonMutingState), commonMutingState, null);
}