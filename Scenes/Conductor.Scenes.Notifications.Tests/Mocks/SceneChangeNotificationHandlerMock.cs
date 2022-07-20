using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

namespace Conductor.Scenes.Notifications.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class SceneChangeNotificationHandlerMock : SceneChangeHandlerBase
{
    public static async Task Handle(
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