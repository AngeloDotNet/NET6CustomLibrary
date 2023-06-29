# SwaggerUI JWT Bearer configuration


## Registering services at Startup (with xml documentation)

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

  services.AddSwaggerGenJWTBearerConfig("My Web Api", "v1", "Add here a description which will be shown in the swagger UI", true, xmlPath);
  //OR services.AddSwaggerGenJWTBearerConfig("My Web Api", "v1", string.Empty, true, xmlPath);
}

//OMISSIS

public void Configure(WebApplication app)
{
  app.UseSwaggerUI("My Web Api v1");
  //OR app.UseSwaggerUINoEmptyRoutePrefix("My Web Api v1");
}
```

<b>Note:</b> If you want to add xml documentation to web api you need to add TRUE value to GenerateDocumentationFile tag to csproj project file.


## Registering services at Startup (without xml documentation)

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  services.AddSwaggerGenJWTBearerConfig("My Web Api", "v1", "Add here a description which will be shown in the swagger UI");
  //OR services.AddSwaggerGenJWTBearerConfig("My Web Api", "v1", string.Empty);
}

//OMISSIS

public void Configure(WebApplication app)
{
  app.UseSwaggerUI("My Web Api v1");
  //OR app.UseSwaggerUINoEmptyRoutePrefix("My Web Api v1");
}
```