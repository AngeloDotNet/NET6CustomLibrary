namespace NET6CustomLibrary.Errors;

public class Result
{
    public bool Succeeded { get; set; }
    public HttpStatusCode StatusCodes { get; set; }
    public int TypeCode { get; set; }
    public string Detail { get; set; }
    public string InstancePath { get; set; }
    public object Message { get; set; }

    internal Result(bool succeeded, HttpStatusCode statusCodes, int typeCode, string detail, HttpContext httpContext, object message = default)
    {
        Succeeded = succeeded;
        StatusCodes = statusCodes;
        TypeCode = typeCode;
        InstancePath = httpContext.Request.Path;
        Detail = detail;
        Message = message;
    }

    public static Result Ok(int typeCode, HttpContext httpContext, object message = default, HttpStatusCode statusCodes = HttpStatusCode.OK)
        => new(succeeded: true, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, message);

    public static Result Created(int typeCode, HttpContext httpContext, object message = default, HttpStatusCode statusCodes = HttpStatusCode.Created)
        => new(succeeded: true, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, message);

    public static Result Accepted(int typeCode, HttpContext httpContext, object message = default, HttpStatusCode statusCodes = HttpStatusCode.Accepted)
        => new(succeeded: true, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, message);

    public static Result NoContent(int typeCode, HttpContext httpContext, HttpStatusCode statusCodes = HttpStatusCode.NoContent)
        => new(succeeded: true, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext);

    public static Result Failure(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.InternalServerError)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);

    public static Result BadRequest(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.BadRequest)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);

    public static Result Unauthorized(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.Unauthorized)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);

    public static Result NotFound(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.NotFound)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);

    public static Result Conflict(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.Conflict)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);

    public static Result UnprocessableEntity(int typeCode, HttpContext httpContext, object errors, HttpStatusCode statusCodes = HttpStatusCode.UnprocessableEntity)
        => new(succeeded: false, statusCodes, typeCode, $"https://httpstatuses.io/{(int)statusCodes}", httpContext, errors);
}