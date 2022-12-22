using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial record FullDeviceMock : IPower
{
    private string _powerState;

    public Task<string> GetPowerStatus(CancellationToken cancellationToken = default) =>
        Task.FromResult(_powerState);

    public Task<string> SwitchPower(string status, CancellationToken cancellationToken = default)
    {
        _powerState = status;
        return Task.FromResult(_powerState);
    }

    public string[] PowerStateResourceKeys() => new[] { "ON", "OFF" };

    public TimeSpan DelayAfterPowerChange => TimeSpan.Zero;
}