using System.Runtime.Serialization;

namespace Conductor.Devices.Interfaces.Exceptions;

[Serializable]
public class MutingStatusException : ApplicationException
{
    protected MutingStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}