using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient : IPower
{
    public async Task<PowerState> GetPowerStatus(CancellationToken cancellationToken = default)
    {
        try
        {
            return (await _telnetClient.SendQueryAsync(new PowerCommand(), cancellationToken)).ToCommonPowerState();
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new UnexpectedResponseException(innerException: e);
        }
        catch (Exception e)
        {
            throw new UnexpectedResponseException($"{GetType()}: Error sending telnet query.", e);
        }
    }

    public Task<PowerState> PowerOn(CancellationToken cancellationToken = default) =>
        SwitchPower(PowerState.On, cancellationToken);

    public Task<PowerState> PowerOff(CancellationToken cancellationToken = default) =>
        SwitchPower(PowerState.Off, cancellationToken);

    public async Task<PowerState> SwitchPower(PowerState status, CancellationToken cancellationToken = default)
    {
        try
        {
            await _telnetClient.SendCommandAsync(status.ToDevicePowerState(), cancellationToken);

            return await GetPowerStatus(cancellationToken);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new UnexpectedResponseException(innerException: e);
        }
        catch (Exception e)
        {
            throw new UnexpectedResponseException($"{GetType()}: Error sending telnet query.", e);
        }
    }

    public async Task<PowerState> PowerToggleAsync(CancellationToken cancellationToken = default) =>
        await SwitchPower((await GetPowerStatus(cancellationToken)).Opposite(), cancellationToken);

    public TimeSpan DelayAfterPowerChange => TimeSpan.FromSeconds(3);
}