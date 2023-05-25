namespace NET6CustomLibrary.CustomResults;

[Obsolete("This method will be deprecated in future releases.", false)]
public class ResponseResult
{
    public bool Succeeded { get; set; }
    public HttpStatusCode StatusCodes { get; set; }
    public int TypeCode { get; set; }
    public object Message { get; set; }
    public string InstancePath { get; set; }
    public string Detail { get; set; }

    public ResponseResult(bool succeeded, int typeCode, HttpContext httpContext, object message, HttpStatusCode statusCodes)
    {
        Succeeded = succeeded;
        StatusCodes = statusCodes;
        TypeCode = typeCode;
        InstancePath = httpContext.Request.Path;
        Detail = $"https://httpstatuses.io/{(int)statusCodes}";
        Message = message;
    }
}