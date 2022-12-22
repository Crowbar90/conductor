using System.Diagnostics.CodeAnalysis;
using Conductor.Devices.Interfaces.Devices;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

[ExcludeFromCodeCoverage]
public partial record FullDeviceMock : IDevice<DeviceConfigurationMock>
{
    public FullDeviceMock(string powerState, string source, string mutingState, string audioMode)
    {
        _powerState = powerState;
        _source = source;
        _mutingState = mutingState;
        _audioMode = audioMode;
    }
}