using Newtonsoft.Json;
using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureUsersResponse
    {
        [JsonProperty("value")]
        public List<AzureUser> Users { get; set; }

        [JsonProperty("odata.nextLink")]
        public string NextLink { get; set; }
    }
}
