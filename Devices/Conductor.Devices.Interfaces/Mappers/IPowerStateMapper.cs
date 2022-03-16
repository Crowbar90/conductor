using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Mappers;

public interface IPowerStateMapper<TPowerState>
    where TPowerState : IConvertible
{
    public PowerState ToCommonPowerState(TPowerState devicePowerState);
    public TPowerState FromCommonPowerState(PowerState commonPowerState);
}