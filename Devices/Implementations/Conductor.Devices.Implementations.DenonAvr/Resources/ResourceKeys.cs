using System.Diagnostics.CodeAnalysis;

namespace Conductor.Devices.Implementations.DenonAvr.Resources;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public static class ResourceKeys
{
    private const string DeviceResourcePrefix = "DENON_AVR";

    public static class AudioMode
    {
        private const string AudioModeResourcePrefix = $"{DeviceResourcePrefix}/AUDIO_MODE";
       
        public static string Direct => $"{AudioModeResourcePrefix}/DIRECT";
        public static string PureDirect => $"{AudioModeResourcePrefix}/PURE_DIRECT";
        public static string Stereo => $"{AudioModeResourcePrefix}/STEREO";
        public static string Auto => $"{AudioModeResourcePrefix}/AUTO";
        public static string DolbyDigital => $"{AudioModeResourcePrefix}/DOLBY_DIGITAL";
        public static string Dts => $"{AudioModeResourcePrefix}/DTS_SURROUND";
        public static string Aurora3D => $"{AudioModeResourcePrefix}/AURO3D";
        public static string Aurora2DSurround => $"{AudioModeResourcePrefix}/AURO2DSURR";
        public static string MultiChannelStereo => $"{AudioModeResourcePrefix}/MCH_STEREO";
        public static string WideScreen => $"{AudioModeResourcePrefix}/WIDE_SCREEN";
        public static string SuperStadium => $"{AudioModeResourcePrefix}/SUPER_STADIUM";
        public static string RockArena => $"{AudioModeResourcePrefix}/ROCK_ARENA";
        public static string JazzClub => $"{AudioModeResourcePrefix}/JAZZ_CLUB";
        public static string ClassicConcert => $"{AudioModeResourcePrefix}/CLASSIC_CONCERT";
        public static string MonoMovie => $"{AudioModeResourcePrefix}/MONO_MOVIE";
        public static string Matrix => $"{AudioModeResourcePrefix}/MATRIX";
        public static string VideoGame => $"{AudioModeResourcePrefix}/VIDEO_GAME";
        public static string Virtual => $"{AudioModeResourcePrefix}/VIRTUAL";

        public static string[] All => new[]
        {
            Direct,
            PureDirect,
            Stereo,
            Auto,
            DolbyDigital,
            Dts,
            Aurora3D,
            Aurora2DSurround,
            MultiChannelStereo,
            WideScreen,
            SuperStadium,
            RockArena,
            JazzClub,
            ClassicConcert,
            MonoMovie,
            Matrix,
            VideoGame,
            Virtual
        };
    }

    public static class MutingState
    {
        private const string MutingStateResourcePrefix = $"{DeviceResourcePrefix}/MUTING";
        
        public static string On => $"{MutingStateResourcePrefix}/ON";
        public static string Off => $"{MutingStateResourcePrefix}/OFF";

        public static string[] All => new[]
        {
            On,
            Off
        };
    }

    public static class PowerState
    {
        private const string PowerStateResourcePrefix = $"{DeviceResourcePrefix}/POWER";
        
        public static string On => $"{PowerStateResourcePrefix}/ON";
        public static string Standby => $"{PowerStateResourcePrefix}/STANDBY";
        public static string Off => $"{PowerStateResourcePrefix}/OFF";

        public static string[] All => new[]
        {
            On,
            Standby,
            Off
        };
    }

    public static class Source
    {
        private const string SourceResourcePrefix = $"{DeviceResourcePrefix}/SOURCE";
        
        public static string Phono => $"{SourceResourcePrefix}/PHONO";
        public static string Cd => $"{SourceResourcePrefix}/CD";
        public static string Dvd => $"{SourceResourcePrefix}/DVD";
        public static string BluRay => $"{SourceResourcePrefix}/BD";
        public static string Tv => $"{SourceResourcePrefix}/TV";
        public static string Sat => $"{SourceResourcePrefix}/SAT_CBL";
        public static string MediaPlayer => $"{SourceResourcePrefix}/MEDIA_PLAYER";
        public static string Game => $"{SourceResourcePrefix}/GAME";
        public static string Tuner => $"{SourceResourcePrefix}/TUNER";
        public static string InternetRadio => $"{SourceResourcePrefix}/INTERNET_RADIO";
        public static string Net => $"{SourceResourcePrefix}/NETWORK";
        public static string Favorites => $"{SourceResourcePrefix}/FAVORITES";
        public static string Aux1 => $"{SourceResourcePrefix}/AUX1";
        public static string Aux2 => $"{SourceResourcePrefix}/AUX2";
        public static string Bluetooth => $"{SourceResourcePrefix}/BLUETOOTH";
        
        public static string[] All => new[]
        {
            Phono,
            Cd,
            Dvd,
            BluRay,
            Tv,
            Sat,
            MediaPlayer,
            Game,
            Tuner,
            InternetRadio,
            Net,
            Favorites,
            Aux1,
            Aux2,
            Bluetooth
        };
    }
}