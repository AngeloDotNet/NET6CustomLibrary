namespace NET6CustomLibrary.ExceptionsMiddleware.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}