using System;

using Newtonsoft.Json;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureCustomGuidFieldJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Guid temp = new Guid();

            if (existingValue != null && Guid.TryParse(existingValue.ToString(), out temp))
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
