namespace NET6CustomLibrary.DateTime.Converters;

public class UtcDateTimeConverter : JsonConverter<System.DateTime>
{
    private readonly string serializationFormat;

    public UtcDateTimeConverter() : this(null)
    {
    }

    public UtcDateTimeConverter(string serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff'Z'";
    }

    public override System.DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetDateTime().ToUniversalTime();

    public override void Write(Utf8JsonWriter writer, System.DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue((value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value)
            .ToString(serializationFormat));
}