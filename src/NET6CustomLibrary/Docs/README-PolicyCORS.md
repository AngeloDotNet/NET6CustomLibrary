# Policy CORS (Cross-Origin Resource Sharing) configuration


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  services.AddControllers();
  services.AddPolicyCors(myPolicyName);
  //OR services.AddFullPolicyCors(myPolicyName); //If you want to add all the CORS configuration options

  //OMISSIS
}

public void Configure(WebApplication app)
{
  //OMISSIS

  app.UseCors(myPolicyName);

  //OMISSIS
}
```

<b>Note:</b> The myPolicyName indicates in string format the name of the policy to be assigned to the CORS configuration.
