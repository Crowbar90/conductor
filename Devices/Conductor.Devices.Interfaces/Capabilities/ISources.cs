using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface ISources
{
    Task<Source> GetActiveSource(CancellationToken cancellationToken = default);

    Task<Source> SetSource(Source source, CancellationToken cancellationToken = default);
    
    TimeSpan DelayAfterSourceChange { get; }
}