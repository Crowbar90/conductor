using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Devices.Implementations.DenonAvr.Configuration;

public record DenonAvrConfiguration(Guid Id, string Host) : IDeviceConfiguration;