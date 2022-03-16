using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Mappers;

public interface IAudioModeMapper<TAudioMode>
    where TAudioMode : struct, IConvertible
{
    public AudioMode ToCommonAudioMode(TAudioMode deviceAudioMode);
    public TAudioMode FromCommonAudioMode(AudioMode commonAudioMode);
}