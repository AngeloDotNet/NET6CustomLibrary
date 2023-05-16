namespace NET6CustomLibrary.Errors;

public class ErrorResult : IErrorResult
{
    public string TitleCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public int TypeCode { get; set; } = 0;
    public string InstancePath { get; set; }
    public List<string> Message { get; set; }

    public ErrorResult ResultUnprocessableEntity(List<string> listErrors, HttpContext httpContext)
    {
        ErrorResult errorResult = new()
        {
            TitleCode = "Validation errors",
            StatusCode = HttpStatusCode.UnprocessableEntity,
            TypeCode = 0,
            InstancePath = httpContext.Request.Path,
            Message = listErrors
        };

        return errorResult;
    }

    //public ErrorResult ResultNotFound(List<string> listErrors, HttpContext httpContext)
    //{
    //    ErrorResult errorResult = new()
    //    {
    //        TitleCode = "Not Found",
    //        StatusCode = HttpStatusCode.NotFound,
    //        TypeCode = 0,
    //        InstancePath = httpContext.Request.Path,
    //        Message = listErrors
    //    };

    //    return errorResult;
    //}
}