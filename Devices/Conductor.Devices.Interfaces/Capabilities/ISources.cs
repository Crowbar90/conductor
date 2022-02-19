using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Conductor.Devices.Interfaces.Capabilities
{
    public interface ISources<TSource>
        where TSource : struct, IConvertible
    {
        IDictionary<TSource, string> Sources { get; }

        Task<TSource?> GetActiveSource(CancellationToken cancellationToken = default);

        Task<TSource?> SetSource(TSource source, CancellationToken cancellationToken = default);
    }
}