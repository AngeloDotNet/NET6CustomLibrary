namespace NET6CustomLibrary.Errors;

public class StatusCodeResult : IErrorResult, IStatusResult
{
    public string TitleCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public int TypeCode { get; set; } = 0;
    public string InstancePath { get; set; }
    public object Message { get; set; }
    //public List<string> Message { get; set; }

    //public StatusCodeResult ResultUnprocessableEntity(List<string> listErrors, HttpContext httpContext)
    public StatusCodeResult ResultUnprocessableEntity(object errors, HttpContext httpContext)
    {
        StatusCodeResult result = new()
        {
            TitleCode = "Unprocessable Entity",
            StatusCode = HttpStatusCode.UnprocessableEntity,
            TypeCode = 0,
            InstancePath = httpContext.Request.Path,
            Message = errors
        };

        return result;
    }

    public StatusCodeResult ResultNotFound(object message, HttpContext httpContext)
    {
        StatusCodeResult result = new()
        {
            TitleCode = "NotFound",
            StatusCode = HttpStatusCode.NotFound,
            TypeCode = 0,
            InstancePath = httpContext.Request.Path,
            Message = message
        };

        return result;
    }
}