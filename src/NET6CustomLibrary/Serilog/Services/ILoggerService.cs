namespace NET6CustomLibrary.Serilog.Services;

public interface ILoggerService
{
    [Obsolete("This method will be deprecated in future releases.", false)]
    ErrorResponse ManageError(string message, int statusCode, int typeCode, HttpContext httpContext);

    /// <summary>
    /// Saving logs of type information
    /// </summary>
    /// <param name="message"></param>
    void SaveLogInformation(string message);

    /// <summary>
    /// Saving logs of type warning
    /// </summary>
    /// <param name="message"></param>
    void SaveLogWarning(string message);

    /// <summary>
    /// Saving logs of type critical
    /// </summary>
    /// <param name="message"></param>
    void SaveLogCritical(string message);

    /// <summary>
    /// Saving logs of type error
    /// </summary>
    /// <param name="message"></param>
    void SaveLogError(string message);

    /// <summary>
    /// Saving logs of type debug
    /// </summary>
    /// <param name="message"></param>
    void SaveLogDebug(string message);
}