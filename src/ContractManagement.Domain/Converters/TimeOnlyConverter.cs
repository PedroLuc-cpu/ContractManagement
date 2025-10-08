using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContractManagement.Domain.Converters
{
    public class TimeOnlyConverter(string? serializationFormat) : JsonConverter<TimeOnly>
    {
        private readonly string _serializationFormat = serializationFormat ?? "HH:mm:ss";
        public TimeOnlyConverter() : this(null) { }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
            {
                return TimeOnly.Parse("00:00:00");
            }
            return TimeOnly.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_serializationFormat));
        }
    }
}
