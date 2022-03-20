using Conductor.Devices.Implementations.DenonAvr.Client;
using Conductor.Devices.Implementations.DenonAvr.Configuration;
using Conductor.Devices.Interfaces.Factory;
using Conductor.Scenes.Notifications;
using MediatR;

namespace Conductor.Devices.Implementations.DenonAvr.Handlers;

public class SceneChangeHandler : SceneChangeHandlerBase, INotificationHandler<SceneChangeNotification>
{
    private readonly IDeviceFactory<DenonAvrClient, DenonAvrConfiguration> _deviceFactory;

    public SceneChangeHandler(
        IDeviceFactory<DenonAvrClient, DenonAvrConfiguration> deviceFactory)
    {
        _deviceFactory = deviceFactory ?? throw new ArgumentNullException(nameof(deviceFactory));
    }
    
    public async Task Handle(
        SceneChangeNotification notification,
        CancellationToken cancellationToken) =>
        await Task.WhenAll(notification.Scene
            .DesiredStates
            .Where(state => state.Device.ClientType == typeof(DenonAvrClient))
            .Select(state =>
                UpdateState<DenonAvrClient, DenonAvrConfiguration>(
                    _deviceFactory.GetInstance(state.Device.Id),
                    state,
                    cancellationToken)));
}