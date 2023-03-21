# Multi Language configuration


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
    var supportedCultures = new[] { "en", "it" };
    services.AddSupportedCultures(supportedCultures);
}

//OMISSIS

public void Configure(WebApplication app)
{
    app.UseLocalizationConfiguration();
}
```

<b>Note:</b> The supportedCultures values can be changed to suit your needs.

But remember to create the resx files of the supported languages in the resources folder, setting the access modifier to Public.
