using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventService.EventService
{
    public class EventConverter : JsonConverter<Event>
    {
        public override Event? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var Name = "Default";
            var Value = 0;
            int Count = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
      
                        case "value" when reader.TokenType == JsonTokenType.Number:
                             Value = reader.GetInt32();  
                            Count++;
                            break;

                        case "value" when reader.TokenType == JsonTokenType.String:
                            throw new JsonException("Value должен быть числом!");
                        case "name":
                            string? name = reader.GetString();
                            if (name != null)
                                Name = name;
                            Count++;
                            break;
                        default:
                            {
                                throw new JsonException("Неверное поле для события!");
                            }
                    }
                }
            }
            if(Count != 2)
            {
                throw new JsonException("JSON должен иметь только 2 поля!");
            }
            return new Event(Name,Value);
        }
        public override void Write(Utf8JsonWriter writer, Event ev, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Name", ev.Name);
            writer.WriteNumber("Value", ev.Value);  
            writer.WriteEndObject();
        }
    }
}
