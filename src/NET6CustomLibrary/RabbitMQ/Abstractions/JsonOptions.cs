namespace NET6CustomLibrary.RabbitMQ.Abstractions;

internal static class JsonOptions
{
    public static JsonSerializerOptions Default { get; } = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };
}