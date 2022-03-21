using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Configurations;
using Conductor.Devices.Interfaces.Devices;
using Conductor.Scenes.Enums;
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
        var powerState = client is IPower powerClient
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
            PowerState = powerState,
            Source = source,
            MutingState = muting,
            AudioMode = audioMode
        };
    }

    private static Task<PowerState?> UpdatePowerState(
        IPower client,
        PowerState? powerState,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            powerState,
            (c, ct) => c.GetPowerStatus(ct),
            (c, s, ct) => c.SwitchPower(s, ct),
            client.DelayAfterPowerChange,
            cancellationToken);

    private static Task<Source?> UpdateSource(
        ISources client,
        Source? source,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            source,
            (c, ct) => c.GetActiveSource(ct),
            (c, s, ct) => c.SetSource(s, ct),
            client.DelayAfterSourceChange,
            cancellationToken);

    private static Task<MutingState?> UpdateMutingState(
        IMuting client,
        MutingState? mutingState,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            mutingState,
            (c, ct) => c.GetMutingStatus(ct),
            (c, s, ct) => c.SwitchMuting(s, ct),
            client.DelayAfterMutingChange,
            cancellationToken);

    private static Task<AudioMode?> UpdateAudioMode(
        IAudioMode client,
        AudioMode? audioMode,
        CancellationToken cancellationToken) =>
        UpdateDeviceState(
            client,
            audioMode,
            (c, ct) => c.GetActiveAudioMode(ct),
            (c, s, ct) => c.SetAudioMode(s, ct),
            client.DelayAfterAudioModeChange,
            cancellationToken);

    private static async Task<TState?> UpdateDeviceState<TClient, TState>(
        TClient client,
        TState? state,
        Func<TClient, CancellationToken, Task<TState>> getStateFn,
        Func<TClient, TState, CancellationToken, Task<TState>> setStateFn,
        TimeSpan delay,
        CancellationToken cancellationToken)
        where TState : struct
    {
        if (!state.HasValue || (await getStateFn(client, cancellationToken)).Equals(state))
            return state;

        var result = await setStateFn(client, state.Value, cancellationToken);
        await Task.Delay(delay, cancellationToken);

        return result;
    }
}