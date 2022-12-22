using System.Diagnostics.CodeAnalysis;
using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class AudioModeMappingExtensions
{
    private const string AudioModeCommandPrefix = "MS";

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    private static readonly TwoWayDictionary<string, Command> AudioModeMapping = new()
    {
        { Resources.ResourceKeys.AudioMode.Direct, PowerCommand.Parse($"{AudioModeCommandPrefix}DIRECT") },
        { Resources.ResourceKeys.AudioMode.PureDirect, PowerCommand.Parse($"{AudioModeCommandPrefix}PURE DIRECT") },
        { Resources.ResourceKeys.AudioMode.Stereo, PowerCommand.Parse($"{AudioModeCommandPrefix}STEREO") },
        { Resources.ResourceKeys.AudioMode.Auto, PowerCommand.Parse($"{AudioModeCommandPrefix}AUTO") },
        { Resources.ResourceKeys.AudioMode.DolbyDigital, PowerCommand.Parse($"{AudioModeCommandPrefix}DOLBY DIGITAL") },
        { Resources.ResourceKeys.AudioMode.Dts, PowerCommand.Parse($"{AudioModeCommandPrefix}DTS SURROUND") },
        { Resources.ResourceKeys.AudioMode.Aurora3D, PowerCommand.Parse($"{AudioModeCommandPrefix}AURO3D") },
        { Resources.ResourceKeys.AudioMode.Aurora2DSurround, PowerCommand.Parse($"{AudioModeCommandPrefix}AURO2DSURR") },
        { Resources.ResourceKeys.AudioMode.MultiChannelStereo, PowerCommand.Parse($"{AudioModeCommandPrefix}MCH STEREO") },
        { Resources.ResourceKeys.AudioMode.WideScreen, PowerCommand.Parse($"{AudioModeCommandPrefix}WIDE SCREEN") },
        { Resources.ResourceKeys.AudioMode.SuperStadium, PowerCommand.Parse($"{AudioModeCommandPrefix}SUPER STADIUM") },
        { Resources.ResourceKeys.AudioMode.RockArena, PowerCommand.Parse($"{AudioModeCommandPrefix}ROCK ARENA") },
        { Resources.ResourceKeys.AudioMode.JazzClub, PowerCommand.Parse($"{AudioModeCommandPrefix}JAZZ CLUB") },
        { Resources.ResourceKeys.AudioMode.ClassicConcert, PowerCommand.Parse($"{AudioModeCommandPrefix}CLASSIC CONCERT") },
        { Resources.ResourceKeys.AudioMode.MonoMovie, PowerCommand.Parse($"{AudioModeCommandPrefix}MONO MOVIE") },
        { Resources.ResourceKeys.AudioMode.Matrix, PowerCommand.Parse($"{AudioModeCommandPrefix}MATRIX") },
        { Resources.ResourceKeys.AudioMode.VideoGame, PowerCommand.Parse($"{AudioModeCommandPrefix}VIDEO GAME") },
        { Resources.ResourceKeys.AudioMode.Virtual, PowerCommand.Parse($"{AudioModeCommandPrefix}VIRTUAL") }
    };

    internal static string ToCommonAudioMode(this Command deviceAudioMode) =>
        AudioModeMapping.TryGetValue(deviceAudioMode, out var commonAudioMode)
            ? commonAudioMode
            : throw new ArgumentOutOfRangeException(nameof(deviceAudioMode), deviceAudioMode, null);

    internal static Command ToDeviceAudioMode(this string commonAudioMode) =>
        AudioModeMapping.TryGetValue(commonAudioMode, out var deviceAudioMode)
            ? deviceAudioMode
            : throw new ArgumentOutOfRangeException(nameof(commonAudioMode), commonAudioMode, null);
}