namespace NET6CustomLibrary.ExceptionsMiddleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionType = exception.GetType();

        HttpStatusCode status;
        HttpStatusCode errorCode;

        string errorStatus;
        string errorDetails;
        string errorMessage;
        //string stackTrace;

        if (exceptionType == typeof(BadRequestException))
        {
            status = HttpStatusCode.BadRequest;
            errorCode = status;
            errorDetails = $"https://httpstatuses.io/{(int)status}";
            errorStatus = "BadRequest";
            errorMessage = exception.Message;
            //stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            status = HttpStatusCode.NotFound;
            errorCode = status;
            errorDetails = $"https://httpstatuses.io/{(int)status}";
            errorStatus = "NotFound";
            errorMessage = exception.Message;
            //stackTrace = exception.StackTrace;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            errorCode = status;
            errorDetails = $"https://httpstatuses.io/{(int)status}";
            errorStatus = "InternalServerError";
            errorMessage = exception.Message;
            //stackTrace = exception.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new ExcMidResult()
        {
            ErrorStatus = errorStatus,
            ErrorCode = errorCode,
            ErrorDetails = errorDetails,
            ErrorMessage = errorMessage
            //ErrorMessage = errorMessage,
            //StackTrace = stackTrace
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
