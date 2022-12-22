namespace Conductor.Devices.Implementations.DenonAvr.Enums;

public enum AudioMode
{
    Direct = 0,
    PureDirect = 1,
    Stereo = 2,
    Auto = 3,
    DolbyDigital = 4,
    Dts = 5,
    Aurora3D = 6,
    Aurora2DSurround = 7,
    MultiChannelStereo = 8,
    WideScreen = 9,
    SuperStadium = 10,
    RockArena = 11,
    JazzClub = 12,
    ClassicConcert = 13,
    MonoMovie = 14,
    Matrix = 15,
    VideoGame = 16,
    Virtual = 17
}

public static class AudioModeResourceKey
{
    public static string Direct => "DENON_AVR/AUDIO_MODE/DIRECT";
}