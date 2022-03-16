using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Connectivity;
using Conductor.Devices.Interfaces.Exceptions;
using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;
using I8Beef.Denon.TelnetClient;

namespace Conductor.Devices.Implementations.DenonAvr;

public class DenonAvrClient :
    INetworkDevice,
    IPower,
    ISources,
    IDisposable
{
    private readonly Client _telnetClient;

    public DenonAvrClient(Client telnetClient)
    {
        _telnetClient = telnetClient ?? throw new ArgumentNullException(nameof(telnetClient));

        _telnetClient.Connect();
    }

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

    public async Task<PowerState> PowerToggleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var desiredPowerState = (await GetPowerStatus(cancellationToken)).Opposite();

            return await SwitchPower(desiredPowerState, cancellationToken);
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

    public TimeSpan DelayAfterPowerChange => TimeSpan.FromSeconds(3);

    public async Task<Source> GetActiveSource(CancellationToken cancellationToken = default)
    {
        try
        {
            return (await _telnetClient.SendQueryAsync(new InputCommand(), cancellationToken)).ToCommonSource();
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

    public async Task<Source> SetSource(Source source, CancellationToken cancellationToken = default)
    {
        try
        {
            await _telnetClient.SendCommandAsync(source.ToDeviceSource(), cancellationToken);

            return await GetActiveSource(cancellationToken);
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

    public TimeSpan DelayAfterSourceChange => TimeSpan.FromSeconds(2);

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _telnetClient.Dispose();
    }
}