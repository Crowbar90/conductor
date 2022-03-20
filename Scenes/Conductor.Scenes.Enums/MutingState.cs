namespace Conductor.Scenes.Enums;

public enum MutingState
{
    Muted,
    Unmuted
}

public static class MutingStateExtensions
{
    public static MutingState Opposite(this MutingState state)
    {
        return state switch
        {
            MutingState.Muted => MutingState.Unmuted,
            MutingState.Unmuted => MutingState.Muted,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}