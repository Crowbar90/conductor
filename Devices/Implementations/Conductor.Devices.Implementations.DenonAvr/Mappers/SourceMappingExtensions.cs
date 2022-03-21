using Conductor.Scenes.Enums;
using Crowbar90.Common.Utilities;
using Crowbar90.Common.Utilities.Generics;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class SourceMappingExtensions
{
    private const string InputCommandPrefix = "SI";

    private static readonly TwoWayDictionary<Source, Command> SourceMapping = new()
    {
        { Source.Phono, PowerCommand.Parse($"{InputCommandPrefix}PHONO") },
        { Source.Cd, PowerCommand.Parse($"{InputCommandPrefix}CD") },
        { Source.Dvd, PowerCommand.Parse($"{InputCommandPrefix}DVD") },
        { Source.BluRay, PowerCommand.Parse($"{InputCommandPrefix}BD") },
        { Source.Tv, PowerCommand.Parse($"{InputCommandPrefix}TV") },
        { Source.Sat, PowerCommand.Parse($"{InputCommandPrefix}SAT/CBL") },
        { Source.MediaPlayer, PowerCommand.Parse($"{InputCommandPrefix}MPLAY") },
        { Source.Game, PowerCommand.Parse($"{InputCommandPrefix}GAME") },
        { Source.Tuner, PowerCommand.Parse($"{InputCommandPrefix}TUNER") },
        { Source.InternetRadio, PowerCommand.Parse($"{InputCommandPrefix}IRADIO") },
        { Source.Net, PowerCommand.Parse($"{InputCommandPrefix}SERVER") },
        { Source.Favorites, PowerCommand.Parse($"{InputCommandPrefix}FAVORITES") },
        { Source.Aux1, PowerCommand.Parse($"{InputCommandPrefix}AUX1") },
        { Source.Aux2, PowerCommand.Parse($"{InputCommandPrefix}AUX2") },
        { Source.Bluetooth, PowerCommand.Parse($"{InputCommandPrefix}BT") }
    };
    
    internal static Source ToCommonSource(this Command deviceSource) =>
        SourceMapping.TryGetValue(deviceSource, out var commonSource)
            ? commonSource
            : throw new ArgumentOutOfRangeException(nameof(deviceSource), deviceSource, null);

    internal static Command ToDeviceSource(this Source commonSource) =>
        SourceMapping.TryGetValue(commonSource, out var deviceSource)
            ? deviceSource
            : throw new ArgumentOutOfRangeException(nameof(commonSource), commonSource, null);
}