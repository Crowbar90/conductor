using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial class FullDeviceMock : IAudioMode
{
    private AudioMode _audioMode;

    public Task<AudioMode> GetActiveAudioMode(CancellationToken cancellationToken = default) =>
        Task.FromResult(_audioMode);

    public Task<AudioMode> SetAudioMode(AudioMode audioMode, CancellationToken cancellationToken = default)
    {
        _audioMode = audioMode;
        return Task.FromResult(_audioMode);
    }

    public TimeSpan DelayAfterAudioModeChange => TimeSpan.Zero;
}