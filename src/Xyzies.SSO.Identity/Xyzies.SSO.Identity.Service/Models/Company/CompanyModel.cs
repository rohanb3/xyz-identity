using Newtonsoft.Json;
using Xyzies.SSO.Identity.Data.Entity;

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

        /// <summary>
        /// Status key from CP
        /// </summary>
        public RequestStatus RequestStatus { get; set; }
    }
}
