﻿using System;
using Newtonsoft.Json;

namespace Xyzies.SSO.Identity.Data.Entity.Azure.AzureAdGraphApi
{
    public class AzureAdApiCredentials
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
