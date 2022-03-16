namespace Conductor.Scenes.Enums;

public enum PowerState
{
    On,
    Off
}

public static class PowerStateExtensions
{
    public static PowerState Opposite(this PowerState state)
    {
        return state switch
        {
            PowerState.On => PowerState.Off,
            PowerState.Off => PowerState.On,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}