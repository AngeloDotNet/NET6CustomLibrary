using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NET6CustomLibrary.DateTime.Converters;
using NET6CustomLibrary.Serilog.Services;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

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
}