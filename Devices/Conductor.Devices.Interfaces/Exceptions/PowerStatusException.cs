using System.Runtime.Serialization;

namespace Conductor.Devices.Interfaces.Exceptions;

[Serializable]
public class PowerStatusException : ApplicationException
{
    protected PowerStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}