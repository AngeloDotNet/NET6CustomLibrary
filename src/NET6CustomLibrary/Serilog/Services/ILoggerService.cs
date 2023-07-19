namespace NET6CustomLibrary.Serilog.Services;

public interface ILoggerService
{
    [Obsolete("This method will be deprecated in future releases.", true)]
    ErrorResponse ManageError(string message, int statusCode, int typeCode, HttpContext httpContext);

    /// <summary>
    /// Saving logs of type INFORMATION
    /// </summary>
    /// <param name="message"></param>
    void SaveLogInformation(string message);

    /// <summary>
    /// Saving logs of type WARNING
    /// </summary>
    /// <param name="message"></param>
    void SaveLogWarning(string message);

    /// <summary>
    /// Saving logs of type CRITICAL
    /// </summary>
    /// <param name="message"></param>
    void SaveLogCritical(string message);

    /// <summary>
    /// Saving logs of type ERROR
    /// </summary>
    /// <param name="message"></param>
    void SaveLogError(string message);

    /// <summary>
    /// Saving logs of type DEBUG
    /// </summary>
    /// <param name="message"></param>
    void SaveLogDebug(string message);
}