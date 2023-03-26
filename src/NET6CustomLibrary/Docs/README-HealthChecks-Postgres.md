# Health Checks configuration for Postgresql database


## Configuration to add to the appsettings.json file

```json
"ConnectionStrings": {
  "Default": "Host=[SERVER];Port=5432;Database=[DATABASE];Username=[USERNAME];Password=[PASSWORD]"
},
```

<b>Note:</b> The default port for Postgresql is 5432, but it can be changed as needed according to your needs.


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
    var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
    services.AddPostgresHealthChecks(connectionString, "Postgres");
}

//OMISSIS

public void Configure(WebApplication app)
{
  //OMISSIS

  app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
      endpoints.AddDatabaseHealthChecks("/status");
  }
}
```