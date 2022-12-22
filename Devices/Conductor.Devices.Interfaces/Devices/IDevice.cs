using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Devices.Interfaces.Devices;

// ReSharper disable once UnusedTypeParameter
public interface IDevice<TConfiguration> where TConfiguration : IDeviceConfiguration
{ }