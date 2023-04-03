# Date and Time only configuration


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  services.AddDateTimeOnlyAttributes();

  services.AddControllers()
    .AddDateTimeJsonOptions();
}
```


## Added options in swagger configuration

Consult the documentation by clicking [here](https://github.com/AngeloDotNet/NET6CustomLibrary/blob/main/src/NET6CustomLibrary/Docs/README-SwaggerDateTime.md).