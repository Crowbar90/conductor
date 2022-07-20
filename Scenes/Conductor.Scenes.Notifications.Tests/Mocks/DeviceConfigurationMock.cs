using System;
using System.Diagnostics.CodeAnalysis;
using Conductor.Devices.Interfaces.Configurations;

namespace Conductor.Scenes.Notifications.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class DeviceConfigurationMock : IDeviceConfiguration
{
    public Guid Id => Guid.Parse("00000000-0000-0000-0000-000000000001");
}