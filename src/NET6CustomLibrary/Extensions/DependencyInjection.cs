namespace NET6CustomLibrary.Extensions;

public static class DependencyInjection
{
    #region "SERILOG"
    public static IServiceCollection AddSerilogServices(this IServiceCollection services)
    {
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

    #region "MULTI LANGUAGE"
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

    #region "DATE and TIME ONLY"
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

    //[Obsolete("This method is deprecated. Please indicate the TypeDescriptors as indicated in the README", false)]
    public static IServiceCollection AddDateTimeOnlyAttributes(this IServiceCollection services)
    {
        TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
        TypeDescriptor.AddAttributes(typeof(TimeOnly), new TypeConverterAttribute(typeof(TimeOnlyTypeConverter)));

        return services;
    }

    public static SwaggerGenOptions AddDateTimeSwaggerGenOptions(this SwaggerGenOptions options)
    {
        options.MapType<DateOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date"
        });

        options.MapType<TimeOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "time",
            Example = new OpenApiString(TimeOnly.FromDateTime(System.DateTime.Now).ToString("HH:mm:ss"))
        });

        return options;
    }
    #endregion

    #region "JSON"
    public static IMvcBuilder AddSimpleJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        return builder;
    }
    #endregion

    #region "EFCORE DBContext Generics"
    public static IServiceCollection AddDbContextGenericsMethods<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        services.AddScoped<DbContext, TDbContext>();
        services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));
        services.AddScoped(typeof(IDatabaseRepository<,>), typeof(DatabaseRepository<,>));
        services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));

        return services;
    }
    #endregion

    #region "EFCORE DBContext"
    public static IServiceCollection AddDbContextUseMySql<TDbContext>(this IServiceCollection services, string connectionString, int retryOnFailure) where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(optionBuilder =>
        {
            if (retryOnFailure > 0)
            {
                optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
                {
                    // Abilito il connection resiliency (Provider di Postgres è soggetto a errori transienti)
                    // Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                    options.EnableRetryOnFailure(retryOnFailure);
                });
            }
            else
            {
                optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        });
        return services;
    }
    #endregion

    #region "DATABASE HEALTH CHECKS"
    public static IServiceCollection AddMySqlHealthChecks(this IServiceCollection services, string connectionString, string nameAsyncCheck)
    {
        services.AddHealthChecks()
            .AddAsyncCheck(nameAsyncCheck, async () =>
            {
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy(ex.Message, ex);
                }

                return HealthCheckResult.Healthy();
            });

        return services;
    }

    public static IServiceCollection AddPostgresHealthChecks(this IServiceCollection services, string connectionString, string nameAsyncCheck)
    {
        services.AddHealthChecks()
            .AddAsyncCheck(nameAsyncCheck, async () =>
            {
                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy(ex.Message, ex);
                }

                return HealthCheckResult.Healthy();
            });

        return services;
    }

    public static IEndpointRouteBuilder AddDatabaseHealthChecks(this IEndpointRouteBuilder builder, string pattern)
    {
        builder.MapHealthChecks(pattern, new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    details = report.Entries.Select(e => new
                    {
                        service = e.Key,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                        description = e.Value.Description
                    })
                });

                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
        });

        return builder;
    }
    #endregion

    #region "MAILKIT"
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
}