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

    #region "DB Context"
    public static IServiceCollection AddDbContextGenericsMethods<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        services.AddScoped<DbContext, TDbContext>();
        services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));
        services.AddScoped(typeof(IDatabaseRepository<,>), typeof(DatabaseRepository<,>));
        services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));

        return services;
    }

    /// <summary>
    /// Extension method per aggiungere un DbContext di tipo TDbContext con il provider MySQL / MariaDB
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="retryOnFailure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContextUseMySql<TDbContext>(this IServiceCollection services, string connectionString, int retryOnFailure) where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(optionBuilder =>
        {
            if (retryOnFailure > 0)
            {
                optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
                {
                    // Abilito il connection resiliency (Provider di Mysql / MariaDB è soggetto a errori transienti)
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

    /// <summary>
    /// Extension method per aggiungere un DbContext di tipo TDbContext con il provider Postgres
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="retryOnFailure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContextUsePostgres<TDbContext>(this IServiceCollection services, string connectionString, int retryOnFailure) where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(optionBuilder =>
        {
            if (retryOnFailure > 0)
            {
                optionBuilder.UseNpgsql(connectionString, options =>
                {
                    // Abilito il connection resiliency (Provider di Postgres è soggetto a errori transienti)
                    // Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                    options.EnableRetryOnFailure(retryOnFailure);
                });
            }
            else
            {
                optionBuilder.UseNpgsql(connectionString);
            }
        });

        return services;
    }

    /// <summary>
    /// Extension method per aggiungere un DbContext di tipo TDbContext con il provider SQL Server
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="retryOnFailure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContextUseSQLServer<TDbContext>(this IServiceCollection services, string connectionString, int retryOnFailure) where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(optionBuilder =>
        {
            if (retryOnFailure > 0)
            {
                optionBuilder.UseSqlServer(connectionString, options =>
                {
                    // Abilito il connection resiliency (Provider di SQL Server è soggetto a errori transienti)
                    // Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                    options.EnableRetryOnFailure(retryOnFailure);
                });
            }
            else
            {
                optionBuilder.UseSqlServer(connectionString);
            }
        });
        return services;
    }

    /// <summary>
    /// Extension method per aggiungere un DbContext di tipo TDbContext con il provider SQLite
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContextUseSQLite<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlite(connectionString, options =>
            {
                // Non abilito il connection resiliency, non è supportato dal provider di Sqlite in quanto non soggetto a errori transienti)
            });
        });

        return services;
    }
    #endregion

    #region "HEALTH CHECKS"
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

    public static IServiceCollection AddSqlServerHealthChecks(this IServiceCollection services, string connectionString, string nameAsyncCheck)
    {
        services.AddHealthChecks()
            .AddAsyncCheck(nameAsyncCheck, async () =>
            {
                try
                {
                    using var connection = new SqlConnection(connectionString);
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

    public static IEndpointRouteBuilder AddDatabaseHealthChecks(this IEndpointRouteBuilder builder, string pattern, bool allowAnonymous = false)
    {
        if (!allowAnonymous)
        {
            builder.MapHealthChecks(pattern, new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = System.Text.Json.JsonSerializer.Serialize(new
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
        }
        else
        {
            builder.MapHealthChecks(pattern, new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = System.Text.Json.JsonSerializer.Serialize(new
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
            }).AllowAnonymous();
        }

        return builder;
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
}