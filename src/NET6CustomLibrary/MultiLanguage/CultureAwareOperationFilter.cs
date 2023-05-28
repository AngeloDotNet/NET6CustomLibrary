﻿namespace NET6CustomLibrary.MultiLanguage;

public class CultureAwareOperationFilter : IOperationFilter
{
    private readonly List<IOpenApiAny> supportedLanguages;

    public CultureAwareOperationFilter(IOptions<RequestLocalizationOptions> requestLocalizationOptions)
    {
        supportedLanguages = requestLocalizationOptions.Value
            .SupportedCultures?.Select(c => new OpenApiString(c.TwoLetterISOLanguageName))
            .Cast<IOpenApiAny>()
            .ToList();
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (supportedLanguages?.Count > 1)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HeaderNames.AcceptLanguage,
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Enum = supportedLanguages,
                    Default = supportedLanguages.First()
                }
            });
        }
    }
}