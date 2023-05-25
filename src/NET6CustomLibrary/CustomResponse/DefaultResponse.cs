namespace NET6CustomLibrary.CustomResponse;

public class DefaultResponse
{
    public HttpStatusCode StatusCode { get; }
    public bool Success { get; set; }
    public object Message { get; set; }

    public DefaultResponse(bool success)
    {
        StatusCode = HttpStatusCode.OK;
        Success = success;
    }

    public DefaultResponse(HttpStatusCode statusCode, bool success)
    {
        StatusCode = statusCode;
        Success = success;
    }

    public DefaultResponse(bool success, object message)
    {
        StatusCode = HttpStatusCode.OK;
        Success = success;
        Message = message;
    }
}