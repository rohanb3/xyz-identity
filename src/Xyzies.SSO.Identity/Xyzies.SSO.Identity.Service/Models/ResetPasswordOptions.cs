namespace Xyzies.SSO.Identity.Services.Models
{
    /// <summary>
    /// Options for reset password service
    /// </summary>
    public class ResetPasswordOptions
    {
        /// <summary>
        /// Email address, from which confirmation codes will be sent
        /// </summary>
        public string ServiceEmailAddress { get; set; }
    }
}
