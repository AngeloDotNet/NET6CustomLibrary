namespace NET6CustomLibrary.Serilog.Services;

public class LoggerService : ILoggerService
{
    private readonly List<string> errorList = new();
    private readonly ILogger<LoggerService> logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        this.logger = logger;
    }

    [Obsolete("This method will be deprecated in future releases.", true)]
    public ErrorResponse ManageError(string message, int statusCode, int typeCode, HttpContext httpContext)
    {
        logger.LogWarning(message);

        errorList.Clear();
        errorList.Add(message);

        return new(statusCode, $"https://httpstatuses.com/{statusCode}", typeCode, httpContext.Request.Path, errorList);
    }

    /// <summary>
    /// Saving logs of type INFORMATION
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogInformation(string message) => logger.LogInformation($"[INFORMATION] {message}");

    /// <summary>
    /// Saving logs of type WARNING
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogWarning(string message) => logger.LogWarning($"[WARNING] {message}");

    /// <summary>
    /// Saving logs of type CRITICAL
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogCritical(string message) => logger.LogCritical($"[CRITICAL] {message}");

    /// <summary>
    /// Saving logs of type ERROR
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogError(string message) => logger.LogError($"[ERROR] {message}");

    /// <summary>
    /// Saving logs of type DEBUG
    /// </summary>
    /// <param name="message"></param>
    public void SaveLogDebug(string message) => logger.LogDebug($"[DEBUG] {message}");
}