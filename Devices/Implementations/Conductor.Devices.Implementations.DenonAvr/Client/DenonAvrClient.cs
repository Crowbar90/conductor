using Conductor.Devices.Implementations.DenonAvr.Configuration;
using Conductor.Devices.Interfaces.Devices;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public partial class DenonAvrClient :
    INetworkDevice<DenonAvrConfiguration>,
    IDisposable
{
    private readonly I8Beef.Denon.TelnetClient.Client _telnetClient;

    public DenonAvrClient(I8Beef.Denon.TelnetClient.Client telnetClient)
    {
        _telnetClient = telnetClient ?? throw new ArgumentNullException(nameof(telnetClient));

        _telnetClient.Connect();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _telnetClient.Dispose();
    }
}