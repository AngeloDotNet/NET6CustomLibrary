using Microsoft.AspNetCore.Http;
using NET6CustomLibrary.Serilog.Models;

namespace NET6CustomLibrary.Serilog.Services;

public class LoggerService : ILoggerService
{
    private List<string> errorList = new();

    private readonly ILogger<LoggerService> logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        this.logger = logger;
    }

    public ErrorResponse ManageError(string message, int statusCode, int typeCode, HttpContext httpContext)
    {
        logger.LogWarning(message);

        errorList.Clear();
        errorList.Add(message);

        return new(statusCode, $"https://httpstatuses.com/{statusCode}", typeCode, httpContext.Request.Path, errorList);
    }

    public void SaveLogInformation(string message)
    {
        logger.LogInformation(message);
    }

    public void SaveLogWarning(string message)
    {
        logger.LogWarning(message);
    }

    public void SaveLogCritical(string message)
    {
        logger.LogCritical(message);
    }

    public void SaveLogError(string message)
    {
        logger.LogError(message);
    }

    public void SaveLogDebug(string message)
    {
        logger.LogDebug(message);
    }
}