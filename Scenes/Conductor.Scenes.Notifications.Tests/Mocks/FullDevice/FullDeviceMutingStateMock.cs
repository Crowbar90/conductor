using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial record FullDeviceMock : IMuting
{
    private string _mutingState;

    public Task<string> GetMutingStatus(CancellationToken cancellationToken = default) =>
        Task.FromResult(_mutingState);

    public Task<string> Mute(CancellationToken cancellationToken = default) =>
        SwitchMuting("ON", cancellationToken);

    public Task<string> Unmute(CancellationToken cancellationToken = default) =>
        SwitchMuting("OFF", cancellationToken);

    public Task<string> SwitchMuting(string status, CancellationToken cancellationToken = default)
    {
        _mutingState = status;
        return Task.FromResult(_mutingState);
    }

    public string[] MutingStateResourceKeys() => new[] { "ON", "OFF" };
    
    public TimeSpan DelayAfterMutingChange => TimeSpan.Zero;
}