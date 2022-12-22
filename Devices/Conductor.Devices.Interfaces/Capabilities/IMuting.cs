namespace Conductor.Devices.Interfaces.Capabilities;

public interface IMuting
{
    Task<string> GetMutingStatus(CancellationToken cancellationToken = default);

    Task<string> Mute(CancellationToken cancellationToken = default);
    Task<string> Unmute(CancellationToken cancellationToken = default);
    Task<string> SwitchMuting(string status, CancellationToken cancellationToken = default);
    
    string[] MutingStateResourceKeys();

    TimeSpan DelayAfterMutingChange { get; }
}