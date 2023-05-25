namespace NET6CustomLibrary.CustomResponse;

public class ExceptionResponse : Exception
{
    public HttpStatusCode StatusCode { get; }
    public int TypeCode { get; }
    public string ErrorCode { get; }
    public string ErrorDetail { get; }
    public string ErrorMessage { get; }
    public object ResponseBody { get; }

    public ExceptionResponse(HttpStatusCode statusCode, int typeCode, string errorCode, string errorMessage)
    {
        StatusCode = statusCode;
        TypeCode = typeCode;
        ErrorCode = errorCode;
        ErrorDetail = $"https://httpstatuses.io/{(int)statusCode}";
        ErrorMessage = errorMessage;
    }

    public ExceptionResponse(HttpStatusCode statusCode, int typeCode, string errorCode, string errorMessage, object responseBody)
    {
        StatusCode = statusCode;
        TypeCode = typeCode;
        ErrorCode = errorCode;
        ErrorDetail = $"https://httpstatuses.io/{(int)statusCode}";
        ErrorMessage = errorMessage;
        ResponseBody = responseBody;
    }
}