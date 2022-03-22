using Conductor.Devices.Interfaces.Devices;
using Conductor.Scenes.Enums;

namespace Conductor.Scenes.Notifications.Tests.Mocks.FullDevice;

public partial class FullDeviceMock : IDevice<DeviceConfigurationMock>
{
    public FullDeviceMock(PowerState powerState, Source source, MutingState mutingState, AudioMode audioMode)
    {
        _powerState = powerState;
        _source = source;
        _mutingState = mutingState;
        _audioMode = audioMode;
    }
}