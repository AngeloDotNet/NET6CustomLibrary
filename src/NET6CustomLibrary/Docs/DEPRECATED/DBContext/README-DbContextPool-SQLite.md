# Entity Framework Core DbContext Pool configuration for SQLite database


## Configuration to add to the appsettings.json file

```json
"ConnectionStrings": {
    "Default": "Data Source=Data/MyDatabase.db"
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
  //OMISSIS

  var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

  //OMISSIS

  services.AddDbContextUseSQLite<MyDbContext>(connectionString);
}
```

<b>Note:</b> You need to replace the <b>MyDbContext</b> value with the actual implementation of your DbContext