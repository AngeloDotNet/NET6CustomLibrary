# RedisCache configuration


## Configuration to add to the appsettings.json file

```json
"Redis": {
  "Hostname": "localhost:6379",
  "InstanceName": "RedisCache_",
  "AbsoluteExpireTime": 60, // in minutes
  "SlidingExpireTime": 15 // in minutes
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
  services.AddRedisCacheService(Configuration);
}
```


## Example of use in a web api controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
  private readonly MyService myService;
  private readonly ICacheService cacheService;

  public EmailController(MyService myService, ICacheService cacheService)
  {
    this.cacheService = cacheService;
    this.myService = myService;
  }
  
  [HttpGet]
  public async Task<List<MyListType>> GetMyListTypeAsync()
  {
    logger.LogInformation("GetMyListType");

    var lista = new List<MyListType>();
    var recordKey = $"MyListType_{SequentialGuidGenerator.Instance.NewGuid()}";

    lista = await cacheService.GetCacheAsync<List<MyListType>>(recordKey);

    if (lista is null)
    {
        lista = await utilityService.GetMyListTypeAsync();
        await cacheService.SetCacheAsync(recordKey, lista);
    }

    return lista;
  }
}
```