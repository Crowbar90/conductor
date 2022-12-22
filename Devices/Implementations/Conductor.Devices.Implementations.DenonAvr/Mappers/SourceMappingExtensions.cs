using System.Diagnostics.CodeAnalysis;
using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class SourceMappingExtensions
{
    private const string InputCommandPrefix = "SI";

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    private static readonly TwoWayDictionary<string, Command> SourceMapping = new()
    {
        { Resources.ResourceKeys.Source.Phono, InputCommand.Parse($"{InputCommandPrefix}PHONO") },
        { Resources.ResourceKeys.Source.Cd, InputCommand.Parse($"{InputCommandPrefix}CD") },
        { Resources.ResourceKeys.Source.Dvd, InputCommand.Parse($"{InputCommandPrefix}DVD") },
        { Resources.ResourceKeys.Source.BluRay, InputCommand.Parse($"{InputCommandPrefix}BD") },
        { Resources.ResourceKeys.Source.Tv, InputCommand.Parse($"{InputCommandPrefix}TV") },
        { Resources.ResourceKeys.Source.Sat, InputCommand.Parse($"{InputCommandPrefix}SAT/CBL") },
        { Resources.ResourceKeys.Source.MediaPlayer, InputCommand.Parse($"{InputCommandPrefix}MPLAY") },
        { Resources.ResourceKeys.Source.Game, InputCommand.Parse($"{InputCommandPrefix}GAME") },
        { Resources.ResourceKeys.Source.Tuner, InputCommand.Parse($"{InputCommandPrefix}TUNER") },
        { Resources.ResourceKeys.Source.InternetRadio, InputCommand.Parse($"{InputCommandPrefix}IRADIO") },
        { Resources.ResourceKeys.Source.Net, InputCommand.Parse($"{InputCommandPrefix}SERVER") },
        { Resources.ResourceKeys.Source.Favorites, InputCommand.Parse($"{InputCommandPrefix}FAVORITES") },
        { Resources.ResourceKeys.Source.Aux1, InputCommand.Parse($"{InputCommandPrefix}AUX1") },
        { Resources.ResourceKeys.Source.Aux2, InputCommand.Parse($"{InputCommandPrefix}AUX2") },
        { Resources.ResourceKeys.Source.Bluetooth, InputCommand.Parse($"{InputCommandPrefix}BT") }
    };
    
    internal static string ToCommonSource(this Command deviceSource) =>
        SourceMapping.TryGetValue(deviceSource, out var commonSource)
            ? commonSource
            : throw new ArgumentOutOfRangeException(nameof(deviceSource), deviceSource, null);

    internal static Command ToDeviceSource(this string commonSource) =>
        SourceMapping.TryGetValue(commonSource, out var deviceSource)
            ? deviceSource
            : throw new ArgumentOutOfRangeException(nameof(commonSource), commonSource, null);
}