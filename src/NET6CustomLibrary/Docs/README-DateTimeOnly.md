# Date and Time only configuration


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
  TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
  TypeDescriptor.AddAttributes(typeof(TimeOnly), new TypeConverterAttribute(typeof(TimeOnlyTypeConverter)));

  services.AddControllers()
    .AddDateTimeJsonOptions();
}
```


## Added options in swagger configuration
```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddSwaggerGen(options =>
    {
      //OMISSIS

      options.AddDateTimeSwaggerGenOptions();

      //OMISSIS
    });
}
```