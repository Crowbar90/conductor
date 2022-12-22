using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Scenes.Model;
using Conductor.Scenes.Notifications.Tests.Mocks;
using Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;
using Shouldly;
using Xunit;

namespace Conductor.Scenes.Notifications.Tests;

[ExcludeFromCodeCoverage]
public class SceneChangeHandlerBaseTests
{
    private static FullDeviceMock InitializeDevice() =>
        new("OFF", "TV", "OFF", "MODE_A");

    private static State InitializeState() =>
        new(new Device(Guid.NewGuid(), typeof(FullDeviceMock)))
        {
            PowerState = "ON",
            Source = "SAT",
            MutingState = "OFF",
            AudioMode = "MODE_B"
        };

    private static Scene InitializeScene(string name, params State[] states) =>
        new(name, states.ToArray());

    private static SceneChangeNotification InitializeNotification(Scene scene) =>
        new(scene);

    [Fact]
    public async Task UpdatingStatus_InternalStatusesAreUpdated()
    {
        var device = InitializeDevice();
        var state = InitializeState();
        var scene = InitializeScene("Test", state);
        var notification = InitializeNotification(scene);
        
        var stopwatch = Stopwatch.StartNew();
        await SceneChangeNotificationHandlerMock.Handle(device, notification, CancellationToken.None);
        stopwatch.Stop();
        
        (await device.GetPowerStatus()).ShouldBe(state.PowerState);
        (await device.GetActiveSource()).ShouldBe(state.Source);
        (await device.GetMutingStatus()).ShouldBe(state.MutingState);
        (await device.GetActiveAudioMode()).ShouldBe(state.AudioMode);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThan(1000);
    }
}