using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureUserToPatch : AzureUser
    {
        [JsonIgnore]
        public override string CreationType { get => base.CreationType; set => base.CreationType = value; }
    }
}
