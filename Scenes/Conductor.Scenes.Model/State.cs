using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Model;

public class State
{
    public State(Device device)
    {
        Device = device;
    }

    public Device Device { get; init; }
    
    public PowerState? PowerState { get; init; }
    public Source? Source { get; init; }
    
    public MutingState? MutingState { get; init; }
    public float? Volume { get; init; }
    
    public AudioMode? AudioMode { get; init; } 
}