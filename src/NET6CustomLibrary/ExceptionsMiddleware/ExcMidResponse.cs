namespace NET6CustomLibrary.ExceptionsMiddleware;

public record ExcMidResponse(string ErrorCode, string ErrorStatus, object ErrorMessage, string StackTrace = null);