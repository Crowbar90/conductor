using System.Net;

namespace Conductor.Devices.Implementations.DenonAvr;

public class DenonAvrConfiguration
{
    /// <summary>
    /// IP address of the Denon AVR
    /// </summary>
    public IPAddress? IpAddress { get; init; }
    
    /// <summary>
    /// Refresh interval (default 10 seconds)
    /// </summary>
    public TimeSpan RefreshInterval { get; init; } = TimeSpan.FromSeconds(10);
}