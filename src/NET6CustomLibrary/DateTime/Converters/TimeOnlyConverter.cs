namespace NET6CustomLibrary.DateTime.Converters;

public class TimeOnlyConverter : System.Text.Json.Serialization.JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    //public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    //    => writer.WriteStringValue(value.ToString("HH:mm:ss.fff"));
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("HH:mm:ss"));
}