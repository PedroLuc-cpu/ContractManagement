using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContractManagement.Domain.Converters
{
    public class DateTimeConverter(string? serializationFormat) : JsonConverter<DateTime>
    {
        private readonly string _serializationFormat = serializationFormat ?? "dd/MM/yyyy HH:mm:ss";

        public DateTimeConverter() : this(null)
        {

        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
            {
                return DateTime.Parse("01/01/0001 00:00:00");
            }
            return DateTime.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(_serializationFormat));
    }
}
