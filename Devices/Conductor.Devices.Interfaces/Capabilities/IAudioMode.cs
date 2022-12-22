namespace Conductor.Devices.Interfaces.Capabilities;

public interface IAudioMode
{
    Task<string> GetActiveAudioMode(CancellationToken cancellationToken = default);

    Task<string> SetAudioMode(string audioMode, CancellationToken cancellationToken = default);

    string[] AudioModeResourceKeys();

    TimeSpan DelayAfterAudioModeChange { get; }
}