using Conductor.Devices.Interfaces.Mappers;

namespace Conductor.Devices.Interfaces.Capabilities;

public interface IAudioMode
{ }

public interface IAudioMode<TAudioMode> : IAudioMode
    where TAudioMode : struct, IConvertible
{
    Type AudioModeMapperType => typeof(IAudioModeMapper<TAudioMode>);

    IDictionary<TAudioMode, string> AudioModes { get; }

    Task<TAudioMode?> GetActiveAudioMode(CancellationToken cancellationToken = default);

    Task<TAudioMode?> SetAudioMode(TAudioMode audioMode, CancellationToken cancellationToken = default);
}