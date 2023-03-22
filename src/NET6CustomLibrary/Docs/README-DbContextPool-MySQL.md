# Entity Framework Core DbContext Pool configuration for MySQL database


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
  //OMISSIS

  var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

  //OMISSIS

  services.AddDbContextUseMySql<MyDbContext>(connectionString, 3);
}
```

<b>Note:</b>The value <b>3</b> indicates the number of attempts (retryOnFailure), in order to avoid transient errors.

If you don't want to activate connection resiliency, set the value to <b>zero</b>.

Finally you need to replace the <b>MyDbContext</b> value with the actual implementation of your DbContext