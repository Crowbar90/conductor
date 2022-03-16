using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Scenes.Enums;
using Conductor.Scenes.Model;

namespace Conductor.StateApplier.Business;

public class StateApplier
{
    private readonly IServiceProvider _serviceProvider;

    public StateApplier(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task ApplyState(DesiredState desiredState)
    {
        var deviceClient = _serviceProvider.GetService(desiredState.Device.ClientType);

        if (deviceClient is IPower)
        {
        }
    }
}