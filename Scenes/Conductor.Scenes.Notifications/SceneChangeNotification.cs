using Conductor.Scenes.Model;
using MediatR;

namespace Conductor.Scenes.Notifications;

public class SceneChangeNotification : INotification
{
    public SceneChangeNotification(Scene scene)
    {
        Scene = scene;
    }

    public Scene Scene { get; }
}