# Health Checks configuration for MySQL database


## Configuration to add to the appsettings.json file

```json
"ConnectionStrings": {
  "Default": "Server=[SERVER];Database=[DATABASE];Uid=[USERNAME];Pwd=[PASSWORD];Port=3306"
},
```

<b>Note:</b> The default port for Mysql / MariaDB is 3306, but it can be changed as needed according to your needs.


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
    services.AddMySqlHealthChecks(connectionString, "MySQL");
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