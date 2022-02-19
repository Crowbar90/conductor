using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Conductor.Devices.Interfaces.Capabilities
{
    public interface IPower<TPowerStatus>
    where TPowerStatus : struct, IConvertible
    {
        IDictionary<TPowerStatus, string> PowerStatuses { get; }

        Task<TPowerStatus?> GetPowerStatus(CancellationToken cancellationToken = default);

        Task<TPowerStatus?> PowerOn(CancellationToken cancellationToken = default);
        Task<TPowerStatus?> PowerOff(CancellationToken cancellationToken = default);
        Task<TPowerStatus?> SwitchPower(TPowerStatus status, CancellationToken cancellationToken = default);
        Task<TPowerStatus?> PowerToggleAsync(CancellationToken cancellationToken = default);
    }
}