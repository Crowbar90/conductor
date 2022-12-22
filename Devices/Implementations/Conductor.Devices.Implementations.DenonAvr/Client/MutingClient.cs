using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient : IMuting
{
    public async Task<string> GetMutingStatus(CancellationToken cancellationToken = default)
    {
        try
        {
            return (await _telnetClient.SendQueryAsync(new MuteCommand(), cancellationToken)).ToCommonMutingState();
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

    public Task<string> Mute(CancellationToken cancellationToken = default) =>
        SwitchMuting(Resources.ResourceKeys.MutingState.On, cancellationToken);

    public Task<string> Unmute(CancellationToken cancellationToken = default) =>
        SwitchMuting(Resources.ResourceKeys.MutingState.Off, cancellationToken);

    public async Task<string> SwitchMuting(string status, CancellationToken cancellationToken = default)
    {
        try
        {
            await _telnetClient.SendCommandAsync(status.ToDeviceMutingState(), cancellationToken);

            return await GetMutingStatus(cancellationToken);
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

    public string[] MutingStateResourceKeys() => Resources.ResourceKeys.MutingState.All;

    public TimeSpan DelayAfterMutingChange => TimeSpan.Zero;
}