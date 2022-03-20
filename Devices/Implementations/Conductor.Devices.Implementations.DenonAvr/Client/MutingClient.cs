using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public partial class DenonAvrClient : IMuting
{
    public async Task<MutingState> GetMutingStatus(CancellationToken cancellationToken = default)
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

    public Task<MutingState> Mute(CancellationToken cancellationToken = default) =>
        SwitchMuting(MutingState.Muted, cancellationToken);

    public Task<MutingState> Unmute(CancellationToken cancellationToken = default) =>
        SwitchMuting(MutingState.Unmuted, cancellationToken);

    public async Task<MutingState> SwitchMuting(MutingState status, CancellationToken cancellationToken = default)
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

    public async Task<MutingState> MutingToggleAsync(CancellationToken cancellationToken = default) =>
        await SwitchMuting((await GetMutingStatus(cancellationToken)).Opposite(), cancellationToken);

    public TimeSpan DelayAfterMutingChange => TimeSpan.Zero;
}