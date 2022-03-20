using Conductor.Devices.Interfaces.Configurations;
using Conductor.Devices.Interfaces.Devices;

namespace Conductor.Devices.Interfaces.Factory;

public abstract class DeviceFactoryBase<TDevice, TConfiguration> : IDeviceFactory<TDevice, TConfiguration>
    where TDevice : IDevice<TConfiguration>
    where TConfiguration : IDeviceConfiguration
{
    private static readonly Dictionary<Guid, Lazy<TDevice>> Clients = new();
    protected abstract TDevice ValueFactory(TConfiguration configuration);
    
    public TDevice GetInstance(Guid id) =>
        Clients.TryGetValue(id, out var lazyClient)
            ? lazyClient.Value
            : throw new ArgumentOutOfRangeException(nameof(id));
    
    public void RegisterInstance(TConfiguration configuration) =>
        Clients[configuration.Id] = !Clients.ContainsKey(configuration.Id)
            ? new Lazy<TDevice>(() => ValueFactory(configuration))
            : throw new ArgumentException($"Device {configuration.Id} is already registered", nameof(configuration));
}