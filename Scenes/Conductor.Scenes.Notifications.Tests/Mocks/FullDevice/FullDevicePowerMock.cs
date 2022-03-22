using System;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial class FullDeviceMock : IPower
{
    private PowerState _powerState;

    public Task<PowerState> GetPowerStatus(CancellationToken cancellationToken = default) =>
        Task.FromResult(_powerState);

    public Task<PowerState> PowerOn(CancellationToken cancellationToken = default) =>
        SwitchPower(PowerState.On, cancellationToken);

    public Task<PowerState> PowerOff(CancellationToken cancellationToken = default) =>
        SwitchPower(PowerState.Off, cancellationToken);

    public Task<PowerState> SwitchPower(PowerState status, CancellationToken cancellationToken = default)
    {
        _powerState = status;
        return Task.FromResult(_powerState);
    }

    public Task<PowerState> PowerToggleAsync(CancellationToken cancellationToken = default) =>
        SwitchPower(_powerState.Opposite(), cancellationToken);

    public TimeSpan DelayAfterPowerChange => TimeSpan.Zero;
}