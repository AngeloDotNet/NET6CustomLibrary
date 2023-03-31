namespace NET6CustomLibrary.Swagger;

public static class SwaggerOptions
{
    public static WebApplication AddUseSwaggerUI(this WebApplication app, string title)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{title}");
        });

        return app;
    }

    public static OpenApiInfo AddOptionalOpenApiInfo(this OpenApiInfo openApiInfo, string name, string email, string siteUrl,
        string nameLicense = "MIT License", string urlLicense = "https://opensource.org/licenses/MIT")
    {
        openApiInfo.Contact = new OpenApiContact
        {
            Name = $"{name}",
            Email = $"{email}",
            Url = new Uri($"{siteUrl}"),
        };

        openApiInfo.License = new OpenApiLicense
        {
            Name = $"{nameLicense}",
            Url = new Uri($"{urlLicense}"),
        };

        return openApiInfo;
    }
}