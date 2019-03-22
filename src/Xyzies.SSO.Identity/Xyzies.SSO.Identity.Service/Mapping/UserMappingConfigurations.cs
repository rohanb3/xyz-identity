using Mapster;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Mapping
{
    public static class UserMappingConfigurations
    {
        public static void ConfigureUserMappers()
        {
            TypeAdapterConfig<AzureUser, Profile>.NewConfig()
                .Map(dest => dest.Email, src => GetSignInNameValue(src.SignInNames.FirstOrDefault(signInName => signInName.Type == "emailAddress")))
                .Map(dest => dest.UserName, src => GetSignInNameValue(src.SignInNames.FirstOrDefault(signInName => signInName.Type == "userName")));
        }

        private static string GetSignInNameValue(SignInName name)
        {
            return name?.Value;
        }
    }
}
