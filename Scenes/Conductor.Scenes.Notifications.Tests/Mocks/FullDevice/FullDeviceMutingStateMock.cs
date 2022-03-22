using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial class FullDeviceMock : IMuting
{
    private MutingState _mutingState;

    public Task<MutingState> GetMutingStatus(CancellationToken cancellationToken = default) =>
        Task.FromResult(_mutingState);

    public Task<MutingState> Mute(CancellationToken cancellationToken = default) =>
        SwitchMuting(MutingState.Muted, cancellationToken);

    public Task<MutingState> Unmute(CancellationToken cancellationToken = default) =>
        SwitchMuting(MutingState.Unmuted, cancellationToken);

    public Task<MutingState> SwitchMuting(MutingState status, CancellationToken cancellationToken = default)
    {
        _mutingState = status;
        return Task.FromResult(_mutingState);
    }

    public Task<MutingState> MutingToggleAsync(CancellationToken cancellationToken = default) =>
        SwitchMuting(_mutingState.Opposite(), cancellationToken);

    public TimeSpan DelayAfterMutingChange => TimeSpan.Zero;
}