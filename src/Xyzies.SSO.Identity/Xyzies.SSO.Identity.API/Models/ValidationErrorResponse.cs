using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.API.Models
{
    [JsonConverter(typeof(ValidationErrorConverver))]
    public class ValidationErrorResponse
    {
        public int? Status { get; set; }

        public List<KeyValuePair<string, List<string>>> Errors { get; set; }
    }

    public class ValidationErrorConverver : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(ValidationErrorResponse);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            throw new NotImplementedException();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ValidationErrorResponse response))
                throw new JsonSerializationException("Expected AccountId object value.");

            writer.WriteStartObject();

            writer.WritePropertyName("status");
            writer.WriteValue(response.Status);

            if (response.Errors != null)
            {
                writer.WritePropertyName("errors");
                writer.WriteStartObject();

                foreach (var error in response.Errors)
                {
                    writer.WritePropertyName(error.Key);
                    writer.WriteStartArray();

                    if (error.Value != null)
                    {
                        foreach (var val in error.Value)
                        {
                            writer.WriteValue(val);
                        }
                    }

                    writer.WriteEndArray();
                }

                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }
    }
}
