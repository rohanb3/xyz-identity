using Mapster;
using System.Collections.Generic;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Services.Models.User;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;

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
               .Map(dest => dest.DisplayName, src => src.Name + " " + src.LastName)
               .Map(dest => dest.Surname, src => src.LastName)
               .Map(dest => dest.GivenName, src => src.Name)
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.AvatarUrl, src => src.ImageName);

            TypeAdapterConfig<User, AzureUser>.NewConfig()
               .Map(dest => dest.DisplayName, src => src.Name + " " + src.LastName)
               .Map(dest => dest.Surname, src => src.LastName)
               .Map(dest => dest.GivenName, src => src.Name)
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.AvatarUrl, src => src.ImageName)
               .Map(dest => dest.SignInNames, src => new List<SignInName>()
               {
                   new SignInName()
                   {
                       Type = "userName",
                       Value = src.Name
                   },
                   new SignInName()
                   {
                       Type = "emailAddress",
                       Value = src.Email
                   }
               })
               .Map(dest => dest.PasswordProfile, src => new PasswordProfile()
               {
                   Password = src.Password
               })
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.CreationType, src => "LocalAccount")
               .Map(dest => dest.PasswordPolicies, src => PasswordPolicy.DisablePasswordExpirationAndStrong)
               .Ignore(dest => dest.Email);

        }

        private static string GetSignInNameValue(SignInName name)
        {
            return name?.Value;
        }
    }
}
