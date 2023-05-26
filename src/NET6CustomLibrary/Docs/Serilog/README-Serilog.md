# Serilog configuration


## Configuration to add to the appsettings.json file

```json
"Serilog": {
  "MinimumLevel": "Debug",
  "WriteTo": [
    {
      "Name": "Console",
      "Args": {
        "outputTemplate": "{Timestamp:HH:mm:ss}\t{Level:u3}\t{SourceContext}\t{Message}{NewLine}{Exception}"
      }
    },
    {
      "Name": "File",
      "Args": {
        "path": "Logs/log.txt",
        "rollingInterval": "Day",
        "retainedFileCountLimit": 14,
        "restrictedToMinimumLevel": "Warning",
        "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
      }
    }
  ]
}
```


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
    services.AddSerilogServices();
}

//OMISSIS

public void Configure(WebApplication app)
{
    app.AddSerilogConfigureServices();
}
```


## Registering services at Program

```csharp
public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
            .AddSerilogOptionsBuilder();

        Startup startup = new(builder.Configuration);

        //OMISSIS
    }
```


## Example of use in a web api controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
  private readonly ILoggerService logger; //required using NET6CustomLibrary.Serilog.Services;

  public PeopleController(ILoggerService logger)
  {
     this.logger = logger;
  }

  [HttpGet("people")]
  public async Task<IActionResult> GetPeople()
  {
    //OMISSIS

    logger.SaveLogInformation("YOUR INFORMATION MESSAGE");

    //OMISSIS
  }
  
  //OMISSIS
}
```


## Types of loggers provided in the ILoggerService class

- CRITICAL
- ERROR
- WARNING
- INFORMATION
- DEBUG

<b>Note:</b> Required using NET6CustomLibrary.Serilog.Services of the NET6CustomLibrary Nuget package
