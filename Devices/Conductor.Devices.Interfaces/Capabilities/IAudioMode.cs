using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Conductor.Devices.Interfaces.Capabilities
{
    public interface IAudioMode<TAudioMode>
        where TAudioMode : struct, IConvertible
    {
        IDictionary<TAudioMode, string> AudioModes { get; }

        Task<TAudioMode?> GetActiveAudioMode(CancellationToken cancellationToken = default);

        Task<TAudioMode?> SetAudioMode(TAudioMode audioMode, CancellationToken cancellationToken = default);
    }
}