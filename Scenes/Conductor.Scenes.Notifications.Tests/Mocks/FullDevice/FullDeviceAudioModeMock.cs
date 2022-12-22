using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial record FullDeviceMock : IAudioMode
{
    private string _audioMode;

    public Task<string> GetActiveAudioMode(CancellationToken cancellationToken = default) =>
        Task.FromResult(_audioMode);

    public Task<string> SetAudioMode(string audioMode, CancellationToken cancellationToken = default)
    {
        _audioMode = audioMode;
        return Task.FromResult(_audioMode);
    }

    public string[] AudioModeResourceKeys() => new[] { "MODE_A", "MODE_B" };

    public TimeSpan DelayAfterAudioModeChange => TimeSpan.Zero;
}