using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Configurations;
using Conductor.Devices.Interfaces.Devices;
using Conductor.Scenes.Model;

namespace Conductor.Scenes.Notifications;

public abstract class SceneChangeHandlerBase
{
    protected static async Task<State> UpdateState<TClient, TConfiguration>(
        TClient client,
        State state,
        CancellationToken cancellationToken)
        where TClient : IDevice<TConfiguration>
        where TConfiguration : IDeviceConfiguration
    {
        var power = client is IPower powerClient
            ? await UpdatePowerState(powerClient, state.PowerState, cancellationToken)
            : null;

        var source = client is ISources sourcesClient
            ? await UpdateSource(sourcesClient, state.Source, cancellationToken)
            : null;

        var muting = client is IMuting mutingClient
            ? await UpdateMutingState(mutingClient, state.MutingState, cancellationToken)
            : null;

        var audioMode = client is IAudioMode audioModeClient
            ? await UpdateAudioMode(audioModeClient, state.AudioMode, cancellationToken)
            : null;

        return new State(state.Device)
        {
            PowerState = power,
            Source = source,
            MutingState = muting,
            AudioMode = audioMode
        };
    }

    private static Task<string?> UpdatePowerState(
        IPower client,
        string? powerState,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            powerState,
            (c, ct) => c.GetPowerStatus(ct),
            (c, s, ct) => c.SwitchPower(s, ct),
            client.DelayAfterPowerChange,
            cancellationToken);
    
    private static Task<string?> UpdateSource(
        ISources client,
        string? source,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            source,
            (c, ct) => c.GetActiveSource(ct),
            (c, s, ct) => c.SetSource(s, ct),
            client.DelayAfterSourceChange,
            cancellationToken);

    private static Task<string?> UpdateMutingState(
        IMuting client,
        string? mutingState,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            mutingState,
            (c, ct) => c.GetMutingStatus(ct),
            (c, s, ct) => c.SwitchMuting(s, ct),
            client.DelayAfterMutingChange,
            cancellationToken);

    private static Task<string?> UpdateAudioMode(
        IAudioMode client,
        string? audioMode,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            audioMode,
            (c, ct) => c.GetActiveAudioMode(ct),
            (c, s, ct) => c.SetAudioMode(s, ct),
            client.DelayAfterAudioModeChange,
            cancellationToken);

    private static async Task<string?> UpdateDeviceState<TClient>(
        TClient client,
        string? state,
        Func<TClient, CancellationToken, Task<string>> getStateFn,
        Func<TClient, string, CancellationToken, Task<string>> setStateFn,
        TimeSpan delay,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(state) || (await getStateFn(client, cancellationToken)).Equals(state))
            return state;

        var result = await setStateFn(client, state, cancellationToken);
        await Task.Delay(delay, cancellationToken);

        return result;
    }
}