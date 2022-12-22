namespace Conductor.Devices.Interfaces.Capabilities;

public interface ISources
{
    Task<string> GetActiveSource(CancellationToken cancellationToken = default);

    Task<string> SetSource(string source, CancellationToken cancellationToken = default);

    string[] SourceResourceKeys();
    
    TimeSpan DelayAfterSourceChange { get; }
}