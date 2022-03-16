using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class PowerStateMappingExtensions
{
    private const string PowerCommandPrefix = "PW";
    
    internal static PowerState ToCommonPowerState(this Command devicePowerState)
    {
        if (devicePowerState.Code != PowerCommandPrefix)
            throw new ArgumentOutOfRangeException(nameof(devicePowerState), devicePowerState, null);
        
        return devicePowerState.Value switch
        {
            "ON" => PowerState.On,
            "STANDBY" => PowerState.Off,
            _ => throw new ArgumentOutOfRangeException(nameof(devicePowerState), devicePowerState, null)
        };
    }

    internal static Command ToDevicePowerState(this PowerState commonPowerState) =>
        commonPowerState switch
        {
            PowerState.On => PowerCommand.Parse($"{PowerCommandPrefix}ON"),
            PowerState.Off => PowerCommand.Parse($"{PowerCommandPrefix}STANDBY"),
            _ => throw new ArgumentOutOfRangeException(nameof(commonPowerState), commonPowerState, null)
        };
}