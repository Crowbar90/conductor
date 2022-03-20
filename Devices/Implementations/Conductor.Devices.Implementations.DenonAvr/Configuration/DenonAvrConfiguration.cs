using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Devices.Implementations.DenonAvr.Configuration;

public class DenonAvrConfiguration : IDeviceConfiguration
{
    public DenonAvrConfiguration(Guid id, string host)
    {
        Id = id;
        Host = host;
    }

    /// <summary>
    /// ID of the device
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// IP address of the Denon AVR
    /// </summary>
    public string Host { get; }
}