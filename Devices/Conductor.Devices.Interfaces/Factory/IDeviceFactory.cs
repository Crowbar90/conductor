using Conductor.Devices.Interfaces.Configurations;
using Conductor.Devices.Interfaces.Devices;

namespace Conductor.Devices.Interfaces.Factory;

public interface IDeviceFactory<out TDevice, in TConfiguration>
    where TDevice : IDevice<TConfiguration>
    where TConfiguration : IDeviceConfiguration
{
    public TDevice GetInstance(Guid id);
    public void RegisterInstance(TConfiguration configuration);
}