using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial record FullDeviceMock : ISources
{
    private string _source;

    public Task<string> GetActiveSource(CancellationToken cancellationToken = default) =>
        Task.FromResult(_source);

    public Task<string> SetSource(string source, CancellationToken cancellationToken = default)
    {
        _source = source;
        return Task.FromResult(_source);
    }

    public string[] SourceResourceKeys() => new[] { "TV", "SAT" };

    public TimeSpan DelayAfterSourceChange => TimeSpan.FromSeconds(1);
}