using Conductor.Devices.Implementations.DenonAvr.Mappers;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Exceptions;
using I8Beef.Denon.Commands;

namespace Conductor.Devices.Implementations.DenonAvr.Client;

public sealed partial class DenonAvrClient : ISources
{
    public async Task<string> GetActiveSource(CancellationToken cancellationToken = default)
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

    public async Task<string> SetSource(string source, CancellationToken cancellationToken = default)
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

    public string[] SourceResourceKeys() => Resources.ResourceKeys.Source.All;

    public TimeSpan DelayAfterSourceChange => TimeSpan.FromSeconds(2);
}