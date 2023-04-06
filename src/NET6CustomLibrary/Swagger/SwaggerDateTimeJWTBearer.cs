namespace NET6CustomLibrary.Swagger;

public static class SwaggerDateTimeJWTBearer
{
    public static IServiceCollection AddSwaggerGenDateTimeJWTBearerConfig(this IServiceCollection services, string title,
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

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert the Bearer Token",
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                if (extendSchema)
                    options.UseAllOfToExtendReferenceSchemas();

                if (xmlCommentsPath is not (null or ""))
                    options.IncludeXmlComments(xmlCommentsPath);
            });

        return services;
    }
}