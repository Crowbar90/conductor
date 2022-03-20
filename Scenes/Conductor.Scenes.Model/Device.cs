namespace Conductor.Scenes.Model;

public class Device
{
    public Device(Guid id, Type clientType)
    {
        Id = id;
        ClientType = clientType;
    }

    public Guid Id { get; }
    public Type ClientType { get; }
}