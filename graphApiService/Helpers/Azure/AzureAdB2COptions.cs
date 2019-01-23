﻿namespace graphApiService.Helpers.Azure
{
    public class AzureAdB2COptions
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string ReplyUrl { get; set; }
    }
}
