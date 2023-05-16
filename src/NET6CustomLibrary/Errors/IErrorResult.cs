namespace NET6CustomLibrary.Errors;

public interface IErrorResult
{
    ErrorResult ResultUnprocessableEntity(List<string> listErrors, HttpContext httpContext);
}