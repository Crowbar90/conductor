using System.Diagnostics.CodeAnalysis;
using Conductor.Scenes.Enums;
using Crowbar90.Common.Utilities;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class AudioModeMappingExtensions
{
    private const string AudioModeCommandPrefix = "MS";

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    private static readonly TwoWayDictionary<AudioMode, Command> AudioModeMapping = new()
    {
        { AudioMode.Direct, PowerCommand.Parse($"{AudioModeCommandPrefix}DIRECT") },
        { AudioMode.PureDirect, PowerCommand.Parse($"{AudioModeCommandPrefix}PURE DIRECT") },
        { AudioMode.Stereo, PowerCommand.Parse($"{AudioModeCommandPrefix}STEREO") },
        { AudioMode.Auto, PowerCommand.Parse($"{AudioModeCommandPrefix}AUTO") },
        { AudioMode.DolbyDigital, PowerCommand.Parse($"{AudioModeCommandPrefix}DOLBY DIGITAL") },
        { AudioMode.Dts, PowerCommand.Parse($"{AudioModeCommandPrefix}DTS SURROUND") },
        { AudioMode.Aurora3D, PowerCommand.Parse($"{AudioModeCommandPrefix}AURO3D") },
        { AudioMode.Aurora2DSurround, PowerCommand.Parse($"{AudioModeCommandPrefix}AURO2DSURR") },
        { AudioMode.MultiChannelStereo, PowerCommand.Parse($"{AudioModeCommandPrefix}MCH STEREO") },
        { AudioMode.WideScreen, PowerCommand.Parse($"{AudioModeCommandPrefix}WIDE SCREEN") },
        { AudioMode.SuperStadium, PowerCommand.Parse($"{AudioModeCommandPrefix}SUPER STADIUM") },
        { AudioMode.RockArena, PowerCommand.Parse($"{AudioModeCommandPrefix}ROCK ARENA") },
        { AudioMode.JazzClub, PowerCommand.Parse($"{AudioModeCommandPrefix}JAZZ CLUB") },
        { AudioMode.ClassicConcert, PowerCommand.Parse($"{AudioModeCommandPrefix}CLASSIC CONCERT") },
        { AudioMode.MonoMovie, PowerCommand.Parse($"{AudioModeCommandPrefix}MONO MOVIE") },
        { AudioMode.Matrix, PowerCommand.Parse($"{AudioModeCommandPrefix}MATRIX") },
        { AudioMode.VideoGame, PowerCommand.Parse($"{AudioModeCommandPrefix}VIDEO GAME") },
        { AudioMode.Virtual, PowerCommand.Parse($"{AudioModeCommandPrefix}VIRTUAL") }
    };
    
    internal static AudioMode ToCommonAudioMode(this Command deviceAudioMode) =>
        AudioModeMapping.TryGetValue(deviceAudioMode, out var commonAudioMode)
            ? commonAudioMode
            : throw new ArgumentOutOfRangeException(nameof(deviceAudioMode), deviceAudioMode, null);

    internal static Command ToDeviceAudioMode(this AudioMode commonAudioMode) =>
        AudioModeMapping.TryGetValue(commonAudioMode, out var deviceAudioMode)
            ? deviceAudioMode
            : throw new ArgumentOutOfRangeException(nameof(commonAudioMode), commonAudioMode, null);
}