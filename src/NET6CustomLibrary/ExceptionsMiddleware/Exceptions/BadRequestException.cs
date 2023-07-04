namespace NET6CustomLibrary.ExceptionsMiddleware.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}