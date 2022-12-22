using System.Diagnostics.CodeAnalysis;
using Conductor.Devices.Interfaces.Devices;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

[ExcludeFromCodeCoverage]
public partial record FullDeviceMock : IDevice<DeviceConfigurationMock>
{
    public FullDeviceMock(PowerState powerState, Source source, MutingState mutingState, AudioMode audioMode)
    {
        _powerState = powerState;
        _source = source;
        _mutingState = mutingState;
        _audioMode = audioMode;
    }
}