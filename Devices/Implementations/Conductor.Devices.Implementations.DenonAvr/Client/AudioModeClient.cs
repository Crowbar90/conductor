using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using Conductor.Scenes.Enums;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient : IAudioMode
{
    public async Task<AudioMode> GetActiveAudioMode(CancellationToken cancellationToken = default)
    {
        try
        {
            return (await _telnetClient.SendQueryAsync(new SurroundModeCommand(), cancellationToken)).ToCommonAudioMode();
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

    public async Task<AudioMode> SetAudioMode(AudioMode audioMode, CancellationToken cancellationToken = default)
    {
        try
        {
            await _telnetClient.SendCommandAsync(audioMode.ToDeviceAudioMode(), cancellationToken);

            return await GetActiveAudioMode(cancellationToken);
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

    public TimeSpan DelayAfterAudioModeChange => TimeSpan.FromSeconds(1);
}