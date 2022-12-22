namespace Conductor.Scenes.Model;

public record Scene(string Name, IEnumerable<State> DesiredStates);