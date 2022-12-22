using Conductor.Scenes.Model;
using MediatR;

namespace Conductor.Scenes.Notifications;

public record SceneChangeNotification(Scene Scene) : INotification;