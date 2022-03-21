using Conductor.Scenes.Enums;
using Crowbar90.Common.Utilities;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class PowerStateMappingExtensions
{
    private const string PowerCommandPrefix = "PW";

    private static readonly TwoWayDictionary<PowerState, Command> PowerStateMapping = new()
    {
        { PowerState.On, PowerCommand.Parse($"{PowerCommandPrefix}ON") },
        { PowerState.Off, PowerCommand.Parse($"{PowerCommandPrefix}STANDBY") }
    };

    internal static PowerState ToCommonPowerState(this Command devicePowerState) =>
        PowerStateMapping.TryGetValue(devicePowerState, out var commonPowerState)
            ? commonPowerState
            : throw new ArgumentOutOfRangeException(nameof(devicePowerState), devicePowerState, null);

    internal static Command ToDevicePowerState(this PowerState commonPowerState) =>
        PowerStateMapping.TryGetValue(commonPowerState, out var devicePowerState)
            ? devicePowerState
            : throw new ArgumentOutOfRangeException(nameof(commonPowerState), commonPowerState, null);
}