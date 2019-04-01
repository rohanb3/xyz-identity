using Newtonsoft.Json;
using System;
namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class OdataValue
    {
        public string Item { get; set; }
        public string Value { get; set; }
    }

    public class OdataMessage
    {
        public string Lang { get; set; }
        public string Value { get; set; }
    }

    public class OdataError
    {
        public string Code { get; set; }
        public OdataMessage Message { get; set; }
        public string RequestId { get; set; }
        public DateTime Date { get; set; }
        public OdataValue[] Values { get; set; }
    }

    public class ErrorResponse
    {
        [JsonProperty("odata.error")]
        public OdataError Odata { get; set; }
    }
}
