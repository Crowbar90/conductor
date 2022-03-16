namespace Conductor.Scenes.Model;

public class Scene
{
    public string Name { get; init; }
    
    public IEnumerable<DesiredState> DesiredStates { get; init; }
}