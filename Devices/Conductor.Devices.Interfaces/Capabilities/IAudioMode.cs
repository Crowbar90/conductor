using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IAudioMode
{
    Task<AudioMode> GetActiveAudioMode(CancellationToken cancellationToken = default);

    Task<AudioMode> SetAudioMode(AudioMode audioMode, CancellationToken cancellationToken = default);

    TimeSpan DelayAfterAudioModeChange { get; }
}