# Health Checks configuration for SQL server database


## Configuration to add to the appsettings.json file

```json
"ConnectionStrings": {
  "Default": "Data Source=[SERVER];Initial Catalog=[DATABASE];User ID=[USERNAME];Password=[PASSWORD]"
  //or "Default": "Data Source=[SERVER];Initial Catalog=[DATABASE];User ID=[USERNAME];Password=[PASSWORD];Encrypt=False"
},
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
    var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
    services.AddSqlServerHealthChecks(connectionString, "SQLServer");
}

//OMISSIS

public void Configure(WebApplication app)
{
  //OMISSIS

  app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
      endpoints.AddDatabaseHealthChecks("/status", false); //Use the True parameter if access is to be in AllowAnonymous mode
  }
}
```