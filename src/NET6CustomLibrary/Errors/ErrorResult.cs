namespace NET6CustomLibrary.Errors;

public class ErrorResult
{
    public string TitleCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public int TypeCode { get; set; } = 0;
    public string InstancePath { get; set; }
    public List<string> Message { get; set; }

    public static ErrorResult ResultUnprocessableEntity(List<string> listErrors, HttpContext httpContext)
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

    //public static ErrorResult ResultNotFound(List<string> listErrors, HttpContext httpContext)
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