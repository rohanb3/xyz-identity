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

            TypeAdapterConfig<User, Profile>.NewConfig()
               .Map(dest => dest.ObjectId, src => src.Id)
               .Map(dest => dest.DisplayName, src => src.Name + src.LastName)
               .Map(dest => dest.Surname, src => src.LastName)
               .Map(dest => dest.GivenName, src => src.Name)
               .Map(dest => dest.AccountEnabled, src => (src.IsActive.HasValue && src.IsActive.Value) && (src.IsDeleted.HasValue &&  !src.IsDeleted.Value))
               .Map(dest => dest.AvatarUrl, src => src.ImageName);
        }

        private static string GetSignInNameValue(SignInName name)
        {
            return name?.Value;
        }
    }
}
