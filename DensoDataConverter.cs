using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Smart_Manufacturing
{
    class DensoDataConverter : JsonConverter<DensoData>
    {
        
            public override DensoData Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,

                JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
            
            public override void Write(
                Utf8JsonWriter writer,
                DensoData value,
                JsonSerializerOptions options)
            {
                switch (value)
                {
                    case null:
                        JsonSerializer.Serialize(writer, (DensoData)null, options);
                        break;
                    default:
                        {
                            var type = value.GetType();
                            JsonSerializer.Serialize(writer, value, type, options);
                            break;
                        }
                }
            }
        
    }
}
