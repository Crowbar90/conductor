namespace Conductor.Scenes.Model;

public record State(Device Device)
{
    public string? PowerState { get; init; }
    public string? Source { get; init; }
    
    public string? MutingState { get; init; }
    
    public float? Volume { get; init; }
    
    public string? AudioMode { get; init; } 
}