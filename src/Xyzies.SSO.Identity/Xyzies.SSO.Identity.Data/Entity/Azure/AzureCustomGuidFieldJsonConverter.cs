using System;

using Newtonsoft.Json;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureCustomGuidFieldJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            String jsonObjectValue = serializer.Deserialize<string>(reader);

            Guid temp = new Guid();
            var isParsed = Guid.TryParse(jsonObjectValue, out temp);

            if (isParsed)
            {
                return temp;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid) || objectType == typeof(Guid?);
        }
    }
}
