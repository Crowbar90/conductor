using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial class FullDeviceMock : ISources
{
    private Source _source;

    public Task<Source> GetActiveSource(CancellationToken cancellationToken = default) =>
        Task.FromResult(_source);

    public Task<Source> SetSource(Source source, CancellationToken cancellationToken = default)
    {
        _source = source;
        return Task.FromResult(_source);
    }

    public TimeSpan DelayAfterSourceChange => TimeSpan.FromSeconds(1);
}