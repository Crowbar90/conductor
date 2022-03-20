using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Devices.Interfaces.Devices;

public interface INetworkDevice<TConfiguration> : IDevice<TConfiguration>
    where TConfiguration : IDeviceConfiguration
{ }