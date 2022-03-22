using System;
using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Scenes.Notifications.Tests.Mocks;

public class DeviceConfigurationMock : IDeviceConfiguration
{
    public Guid Id => Guid.Parse("00000000-0000-0000-0000-000000000001");
}