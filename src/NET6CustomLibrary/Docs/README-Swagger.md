# SwaggerUI configuration


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

//OMISSIS

public void Configure(WebApplication app)
{
  app.AddUseSwaggerUI("My Web Api v1");
}
```