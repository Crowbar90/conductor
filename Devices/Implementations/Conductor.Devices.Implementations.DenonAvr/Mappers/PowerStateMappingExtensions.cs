using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class PowerStateMappingExtensions
{
    private const string PowerCommandPrefix = "PW";

    private static readonly TwoWayDictionary<string, Command> PowerStateMapping = new()
    {
        { Resources.ResourceKeys.PowerState.On, PowerCommand.Parse($"{PowerCommandPrefix}ON") },
        { Resources.ResourceKeys.PowerState.Standby, PowerCommand.Parse($"{PowerCommandPrefix}STANDBY") },
        { Resources.ResourceKeys.PowerState.Off, PowerCommand.Parse($"{PowerCommandPrefix}OFF") }
    };

    internal static string ToCommonPowerState(this Command devicePowerState) =>
        PowerStateMapping.TryGetValue(devicePowerState, out var commonPowerState)
            ? commonPowerState
            : throw new ArgumentOutOfRangeException(nameof(devicePowerState), devicePowerState, null);

    internal static Command ToDevicePowerState(this string commonPowerState) =>
        PowerStateMapping.TryGetValue(commonPowerState, out var devicePowerState)
            ? devicePowerState
            : throw new ArgumentOutOfRangeException(nameof(commonPowerState), commonPowerState, null);
}