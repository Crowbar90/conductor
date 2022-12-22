namespace Conductor.Devices.Interfaces.Capabilities;

public interface IPower
{
    Task<string> GetPowerStatus(CancellationToken cancellationToken = default);
    
    Task<string> SwitchPower(string status, CancellationToken cancellationToken = default);

    string[] PowerStateResourceKeys();
    
    TimeSpan DelayAfterPowerChange { get; }
}