namespace NET6CustomLibrary.Extensions;

public static class DependencyInjection
{
    #region "SERILOG"
    public static IServiceCollection AddSerilogServices(this IServiceCollection services)
    {
        services.AddTransient<ILoggerService, LoggerService>();

        return services;
    }

    public static IServiceCollection AddSerilogSeqServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .CreateLogger();

        services.AddTransient<ILoggerService, LoggerService>();

        return services;
    }

    public static WebApplication AddSerilogConfigureServices(this WebApplication application)
    {
        application.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        return application;
    }

    public static WebApplicationBuilder AddSerilogOptionsBuilder(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });

        return builder;
    }
    #endregion

    #region "SUPPORTED CULTURES"
    public static IServiceCollection AddSupportedCultures(this IServiceCollection services, string[] cultures)
    {
        var supportedCultures = cultures;
        var localizationOptions = new RequestLocalizationOptions()
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures)
            .SetDefaultCulture(supportedCultures[0]);

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SupportedCultures = localizationOptions.SupportedCultures;
            options.SupportedUICultures = localizationOptions.SupportedUICultures;
            options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
        });

        return services;
    }

    public static WebApplication UseLocalizationConfiguration(this WebApplication app)
    {
        app.UseRequestLocalization();

        return app;
    }
    #endregion

    #region "DATE and TIME"
    public static IMvcBuilder AddSimpleJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        return builder;
    }

    public static IMvcBuilder AddDateTimeJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
        });

        return builder;
    }

    public static IServiceCollection AddDateTimeOnlyAttributes(this IServiceCollection services)
    {
        TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
        TypeDescriptor.AddAttributes(typeof(TimeOnly), new TypeConverterAttribute(typeof(TimeOnlyTypeConverter)));

        return services;
    }
    #endregion

    #region "SEND EMAIL"
    public static IServiceCollection AddMailKitEmailSenderService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IEmailSender, MailKitEmailSender>()
            .AddSingleton<IEmailClient, MailKitEmailSender>();

        services
            .Configure<SmtpOptions>(configuration.GetSection("Smtp"));

        return services;
    }
    #endregion

    #region "REDIS CACHE"
    public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = configuration.GetSection("Redis");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig["Hostname"];
            options.InstanceName = redisConfig["InstanceName"];
        });

        services.AddTransient<ICacheService, CacheService>();

        services.Configure<RedisOptions>(configuration.GetSection("Redis"));

        return services;
    }
    #endregion

    #region "FLUENT VALIDATION"
    public static IServiceCollection AddFluentValidationService<TAssembly>(this IServiceCollection services) where TAssembly : class
    {
        services.AddScoped<IValidation, Validazione.Validation>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddValidatorsFromAssemblyContaining<TAssembly>();

        return services;
    }
    #endregion

    #region "POLICY CORS"
    public static IServiceCollection AddPolicyCors(this IServiceCollection services, string policyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy($"{policyName}", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddFullPolicyCors(this IServiceCollection services, string policyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy($"{policyName}", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });

        return services;
    }
    #endregion

    //Rif: https://dev.to/moe23/net-6-web-api-global-exceptions-handling-1a46
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
}