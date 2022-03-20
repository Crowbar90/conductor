namespace Conductor.Scenes.Model;

public class Scene
{
    public Scene(string name, IEnumerable<State> desiredStates)
    {
        Name = name;
        DesiredStates = desiredStates;
    }

    public string Name { get; }
    public IEnumerable<State> DesiredStates { get; }
}