using System.Runtime.Serialization;

namespace Conductor.Devices.Interfaces.Exceptions;

[Serializable]
public class UnexpectedResponseException : ApplicationException
{
    public UnexpectedResponseException(string? message) : base(message)
    { }

    public UnexpectedResponseException(string? message = null, Exception? innerException = null) : base(message, innerException)
    { }

    protected UnexpectedResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}