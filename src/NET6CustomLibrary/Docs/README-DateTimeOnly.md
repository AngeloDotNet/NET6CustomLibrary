# Date and Time only configuration


## Add the converters and the comparers in the OnModelCreating method of the DbContext 
```csharp
modelBuilder.Entity<MyEntity>(builder =>
{
  // Date is a DateOnly property and date on database
  builder.Property(x => x.Date)
    .HasConversion<DateOnlyConverter, DateOnlyComparer>();

  // Time is a TimeOnly property and time on database
  builder.Property(x => x.Time)
    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
});

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