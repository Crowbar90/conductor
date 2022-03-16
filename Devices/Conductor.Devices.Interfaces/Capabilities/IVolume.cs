namespace Conductor.Devices.Interfaces.Capabilities;

public interface IVolume
{ }

public interface IVolume<TVolume> : IVolume
{
    Task<TVolume> GetVolume(CancellationToken cancellationToken = default);
        
    Task<TVolume> IncreaseVolume(CancellationToken cancellationToken = default);
    Task<TVolume> DecreaseVolume(CancellationToken cancellationToken = default);
    Task<TVolume> SetVolume(TVolume volume, CancellationToken cancellationToken = default);
}