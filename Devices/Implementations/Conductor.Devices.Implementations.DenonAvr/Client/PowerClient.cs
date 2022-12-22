using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient : IPower
{
    public async Task<string> GetPowerStatus(CancellationToken cancellationToken = default)
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

    public async Task<string> SwitchPower(string status, CancellationToken cancellationToken = default)
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

    public string[] PowerStateResourceKeys() => Resources.ResourceKeys.PowerState.All;

    public TimeSpan DelayAfterPowerChange => TimeSpan.FromSeconds(3);
}