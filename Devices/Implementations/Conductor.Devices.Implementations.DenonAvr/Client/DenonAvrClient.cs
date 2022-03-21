using Conductor.Devices.Implementations.DenonAvr.Configuration;
using Conductor.Devices.Interfaces.Devices;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient :
    INetworkDevice<DenonAvrConfiguration>,
    IDisposable
{
    private readonly I8Beef.Denon.TelnetClient.Client _telnetClient;
    private bool _disposed;

    public DenonAvrClient(I8Beef.Denon.TelnetClient.Client telnetClient)
    {
        _telnetClient = telnetClient ?? throw new ArgumentNullException(nameof(telnetClient));

        _telnetClient.Connect();
    }

    // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        
        if (disposing)
            _telnetClient.Dispose();

        _disposed = true;
    }
}