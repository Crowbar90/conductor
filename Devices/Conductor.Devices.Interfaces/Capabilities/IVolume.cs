using System.Threading;
using System.Threading.Tasks;

namespace Conductor.Devices.Interfaces.Capabilities
{
    public interface IVolume<TVolume>
    {
        Task<TVolume> GetVolume(CancellationToken cancellationToken = default);
        
        Task<TVolume> IncreaseVolume(CancellationToken cancellationToken = default);
        Task<TVolume> DecreaseVolume(CancellationToken cancellationToken = default);
        Task<TVolume> SetVolume(TVolume volume, CancellationToken cancellationToken = default);
    }
}