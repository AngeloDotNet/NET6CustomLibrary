# Serilog configuration with SEQ platform


## Configuration to add to the appsettings.json file (Writing logs to console, to txt file and to SEQ platform)

<b>Note:</b> If the logs must be saved only on SEQ, comment out and/or delete the part of the configuration linked to the FILE and CONSOLE sections.

The minimum level of traced logs is DEBUG.

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
        "restrictedToMinimumLevel": "Information",
        "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog",
        "outputTemplate": "{Timestamp:HH:mm:ss}\t{Level:u3}\t{SourceContext}\t{Message}{NewLine}{Exception}"
      }
    },
    {
      "Name": "Seq",
      "Application": "Sample API",
      "Args": {
        "serverUrl": "http://server-seq:5341",
        "ApiKey": "YOUR-APIKEY",
        "restrictedToMinimumLevel": "Information",
        "outputTemplate": "{Timestamp:HH:mm:ss}\t{Level:u3}\t{SourceContext}\t{Message}{NewLine}{Exception}"
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
  services.AddSerilogSeqServices();
}

//OMISSIS

public void Configure(WebApplication app)
{
  //OMISSIS

  app.AddSerilogConfigureServices();

  //OMISSIS
}
```


## Registering services at Program

```csharp
public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.AddSerilogOptionsBuilder();

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


## Docker SEQ

An example of a Docker configuration of SEQ can be found [here](https://github.com/AngeloDotNet/Docker.Application/tree/master/Seq).

For SEQ (docker version) the first boot occurs without any form of active login.
After the docker is active, navigate to the SETTINGS > USERS section to enable authentication to access the dashboard.


## Types of loggers provided in the ILoggerService class

- CRITICAL
- ERROR
- WARNING
- INFORMATION
- DEBUG

<b>Note:</b> Required using NET6CustomLibrary.Serilog.Services of the NET6CustomLibrary Nuget package
