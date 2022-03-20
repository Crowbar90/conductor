using Conductor.Devices.Implementations.DenonAvr.Client;
using Conductor.Devices.Implementations.DenonAvr.Configuration;
using Conductor.Devices.Interfaces.Factory;

namespace Conductor.Devices.Implementations.DenonAvr.Factory;

public class DenonAvrClientFactory : DeviceFactoryBase<DenonAvrClient, DenonAvrConfiguration>
{
    protected override DenonAvrClient ValueFactory(DenonAvrConfiguration configuration) =>
        new(new I8Beef.Denon.TelnetClient.Client(configuration.Host));
}