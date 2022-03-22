using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

namespace Conductor.Scenes.Notifications.Tests.Mocks;

public class SceneChangeNotificationHandlerMock : SceneChangeHandlerBase
{
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public async Task Handle(
        FullDeviceMock device,
        SceneChangeNotification notification,
        CancellationToken cancellationToken) =>
        await Task.WhenAll(notification.Scene
            .DesiredStates
            .Where(state => state.Device.ClientType == typeof(FullDeviceMock))
            .Select(state =>
                UpdateState<FullDeviceMock, DeviceConfigurationMock>(
                    device,
                    state,
                    cancellationToken)));
}