namespace NET6CustomLibrary.Swagger;

public static class SwaggerSimple
{
    public static IServiceCollection AddSwaggerGenConfig(this IServiceCollection services, string title,
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

                if (extendSchema)
                    options.UseAllOfToExtendReferenceSchemas();

                if (xmlCommentsPath is not (null or ""))
                    options.IncludeXmlComments(xmlCommentsPath);
            });

        return services;
    }
}