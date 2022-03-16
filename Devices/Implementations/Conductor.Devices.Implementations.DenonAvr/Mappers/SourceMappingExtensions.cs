using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Mappers;

internal static class SourceMappingExtensions
{
    private const string InputCommandPrefix = "SI";
    
    internal static Source ToCommonSource(this Command deviceSource)
    {
        if (deviceSource.Code != InputCommandPrefix)
            throw new ArgumentOutOfRangeException(nameof(deviceSource), deviceSource, null);

        return deviceSource.Value switch
        {
            "PHONO" => Source.Phono,
            "CD" => Source.Cd,
            "DVD" => Source.Dvd,
            "BD" => Source.BluRay,
            "TV" => Source.Tv,
            "SAT/CBL" => Source.Sat,
            "MPLAY" => Source.MediaPlayer,
            "GAME" => Source.Game,
            "TUNER" => Source.Tuner,
            "IRADIO" => Source.InternetRadio,
            "SERVER" => Source.Net,
            "FAVORITES" => Source.Favorites,
            "AUX1" => Source.Aux1,
            "AUX2" => Source.Aux2,
            "BT" => Source.Bluetooth,
            _ => throw new ArgumentOutOfRangeException(nameof(deviceSource), deviceSource, null)
        };
    }

    internal static Command ToDeviceSource(this Source commonSource) =>
        commonSource switch {
            Source.Phono => InputCommand.Parse($"{InputCommandPrefix}PHONO"),
            Source.Cd => InputCommand.Parse($"{InputCommandPrefix}CD"),
            Source.Tuner => InputCommand.Parse($"{InputCommandPrefix}TUNER"),
            Source.Dvd => InputCommand.Parse($"{InputCommandPrefix}DVD"),
            Source.BluRay => InputCommand.Parse($"{InputCommandPrefix}BD"),
            Source.Tv => InputCommand.Parse($"{InputCommandPrefix}TV"),
            Source.Sat => InputCommand.Parse($"{InputCommandPrefix}SAT/CBL"),
            Source.Game => InputCommand.Parse($"{InputCommandPrefix}GAME"),
            Source.MediaPlayer => InputCommand.Parse($"{InputCommandPrefix}MPLAY"),
            Source.Aux1 => InputCommand.Parse($"{InputCommandPrefix}AUX1"),
            Source.Aux2 => InputCommand.Parse($"{InputCommandPrefix}AUX2"),
            Source.Net => InputCommand.Parse($"{InputCommandPrefix}NET"),
            Source.InternetRadio => InputCommand.Parse($"{InputCommandPrefix}IRADIO"),
            Source.Favorites => InputCommand.Parse($"{InputCommandPrefix}FAVORITES"),
            Source.Bluetooth => InputCommand.Parse($"{InputCommandPrefix}BT"),
            _ => throw new ArgumentOutOfRangeException(nameof(commonSource), commonSource, null)
        };
}