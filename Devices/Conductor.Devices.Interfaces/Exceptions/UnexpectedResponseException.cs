namespace Conductor.Devices.Interfaces.Exceptions;

public class UnexpectedResponseException : ApplicationException
{
    public UnexpectedResponseException(string? message) : base(message)
    {
    }

    public UnexpectedResponseException(string? message = null, Exception? innerException = null) : base(message, innerException)
    {
    }
}