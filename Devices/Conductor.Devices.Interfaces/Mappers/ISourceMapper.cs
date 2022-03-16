using Conductor.Scenes.Enums;

namespace Conductor.Devices.Interfaces.Mappers;

public interface ISourceMapper<TSource>
    where TSource : struct, IConvertible
{
    public Source ToCommonSource(TSource deviceSource);
    public TSource FromCommonSource(Source commonSource);
}