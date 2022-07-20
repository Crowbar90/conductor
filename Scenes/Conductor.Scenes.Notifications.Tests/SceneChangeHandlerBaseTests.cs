using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Scenes.Enums;
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
        new(PowerState.On, Source.Tv, MutingState.Unmuted, AudioMode.Auto);

    private static State InitializeState() =>
        new(new Device(Guid.NewGuid(), typeof(FullDeviceMock)))
        {
            PowerState = PowerState.On,
            Source = Source.Sat,
            MutingState = MutingState.Unmuted,
            AudioMode = AudioMode.Auto
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
        
        (await device.GetPowerStatus()).ShouldBe(PowerState.On);
        (await device.GetActiveSource()).ShouldBe(Source.Sat);
        (await device.GetMutingStatus()).ShouldBe(MutingState.Unmuted);
        (await device.GetPowerStatus()).ShouldBe(PowerState.On);
        stopwatch.ElapsedMilliseconds.ShouldBeGreaterThan(1000);
    }
}