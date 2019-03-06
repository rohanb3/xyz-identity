namespace IdentityServiceClient.Models.User
{
    public class PasswordProfile
    {
        public string Password { get; set; }
        public bool? ForceChangePasswordNextLogin { get; set; }
        public bool? EnforceChangePasswordPolicy { get; set; }
    }
}
