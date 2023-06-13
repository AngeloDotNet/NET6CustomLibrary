namespace NET6CustomLibrary.Swagger;

public static class SwaggerOptions
{
    public static WebApplication UseSwaggerUI(this WebApplication app, string title)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{title}");
        });

        return app;
    }

    public static WebApplication UseSwaggerUINoEmptyRoutePrefix(this WebApplication app, string title)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{title}");
        });

        return app;
    }
}