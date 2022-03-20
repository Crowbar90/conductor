using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Devices.Interfaces.Devices;

public interface IDevice<TConfiguration> where TConfiguration : IDeviceConfiguration
{ }