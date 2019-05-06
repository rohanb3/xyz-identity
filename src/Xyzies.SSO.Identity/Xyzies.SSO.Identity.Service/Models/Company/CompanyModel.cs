using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.Company
{
    /// <summary>
    /// Company model
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("companyName")]
        public string Name { get; set; }
    }
}
