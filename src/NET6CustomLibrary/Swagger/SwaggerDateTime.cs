namespace NET6CustomLibrary.Swagger;

public static class SwaggerDateTime
{
    public static IServiceCollection AddSwaggerGenDateTimeConfig(this IServiceCollection services, string title,
        string version, string description = "", bool extendSchema = false, string xmlCommentsPath = "")
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.OperationFilter<CultureAwareOperationFilter>();
                options.SwaggerDoc($"{version}", new OpenApiInfo
                {
                    Title = $"{title}",
                    Version = $"{version}",
                    Description = $"{description}",
                });

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

                if (extendSchema)
                    options.UseAllOfToExtendReferenceSchemas();

                if (xmlCommentsPath is not (null or ""))
                    options.IncludeXmlComments(xmlCommentsPath);
            });

        return services;
    }
}