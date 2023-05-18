namespace NET6CustomLibrary.Serilog.Services;

public class LoggerService : ILoggerService
{
    private readonly List<string> errorList = new();
    private readonly ILogger<LoggerService> logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        this.logger = logger;
    }

    [Obsolete("This method will be deprecated in future releases.", false)]
    public ErrorResponse ManageError(string message, int statusCode, int typeCode, HttpContext httpContext)
    {
        logger.LogWarning(message);

        errorList.Clear();
        errorList.Add(message);

        return new(statusCode, $"https://httpstatuses.com/{statusCode}", typeCode, httpContext.Request.Path, errorList);
    }

    /// <summary>
    /// Saving logs of type information
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogInformation(string message) => logger.LogInformation(message);

    /// <summary>
    /// Saving logs of type warning
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogWarning(string message) => logger.LogWarning(message);

    /// <summary>
    /// Saving logs of type critical
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogCritical(string message) => logger.LogCritical(message);

    /// <summary>
    /// Saving logs of type error
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogError(string message) => logger.LogError(message);

    /// <summary>
    /// Saving logs of type debug
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogDebug(string message) => logger.LogDebug(message);
}