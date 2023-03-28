namespace NET6CustomLibrary.DateTime.Converters;

//public class DateOnlyConverter : JsonConverter<DateOnly>
//{
//    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var value = reader.GetString();
//        return DateOnly.Parse(value);
//    }

//    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
//        => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
//}

public class DateOnlyConverter : ValueConverter<DateOnly, System.DateTime>
{
    public DateOnlyConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}