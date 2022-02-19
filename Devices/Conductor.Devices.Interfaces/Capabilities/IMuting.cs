using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Conductor.Devices.Interfaces.Capabilities
{
    public interface IMuting<TMutingStatus>
    where TMutingStatus : struct, IConvertible
    {
        IDictionary<TMutingStatus, string> MutingStatuses { get; }

        Task<TMutingStatus?> GetMutingStatus(CancellationToken cancellationToken = default);

        Task<TMutingStatus?> Mute(CancellationToken cancellationToken = default);
        Task<TMutingStatus?> Unmute(CancellationToken cancellationToken = default);
        Task<TMutingStatus?> SwitchMuting(TMutingStatus status, CancellationToken cancellationToken = default);
        Task<TMutingStatus?> MutingToggleAsync(CancellationToken cancellationToken = default);
    }
}