using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xyzies.SSO.Identity.Data.Entity.Azure;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class ProfileCreatable : BaseProfile
    {
        [DefaultValue("LocalAccount")]
        public string CreationType { get; set; }

        [Required(ErrorMessage = "Password profile is required")]
        public PasswordProfile PasswordProfile { get; set; }

        [Required(ErrorMessage = "There must be at least one SignIn name")]
        public List<SignInName> SignInNames { get; set; }
    }
}
