using Mapster;
using System;
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
               .Map(dest => dest.DisplayName, src => ReplaceNullOrEmpty($"{src.Name ?? ""} {src.LastName ?? ""}".Trim()))
               .Map(dest => dest.Surname, src => src.LastName)
               .Map(dest => dest.GivenName, src => src.Name)
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.AvatarUrl, src => src.ImageName);

            TypeAdapterConfig<Profile, User>.NewConfig()
               .Map(dest => dest.IsActive, src => src.AccountEnabled)
               .Map(dest => dest.LastName, src => src.Surname)
               .Map(dest => dest.Name, src => src.GivenName)
               .Map(dest => dest.IsActive, src => src.AccountEnabled)
               .Map(dest => dest.ImageName, src => src.AvatarUrl)
               .Map(dest => dest.Password, src => "Secret12345")
               .Map(dest => dest.CreatedBy, src => 1)
               .Map(dest => dest.ModifiedBy, src => 1)
               .Map(dest => dest.IsDeleted, src => false)
               .Map(dest => dest.UserGuid, src => src.ObjectId)
               .Map(dest => dest.CreatedDate, src => DateTime.UtcNow)
               .Map(dest => dest.ModifiedDate, src => DateTime.UtcNow);

            TypeAdapterConfig<User, AzureUser>.NewConfig()
               .Map(dest => dest.CPUserId, src => src.Id)
               .Map(dest => dest.DisplayName, src => ReplaceNullOrEmpty($"{src.Name ?? ""} {src.LastName ?? ""}".Trim()))
               .Map(dest => dest.Surname, src => src.LastName)
               .Map(dest => dest.GivenName, src => src.Name)
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.AvatarUrl, src => src.ImageName)
               .Map(dest => dest.SignInNames, src => new List<SignInName>()
               {
                   new SignInName()
                   {
                       Type = "emailAddress",
                       Value = src.Email
                   }
               })
               .Map(dest => dest.PasswordProfile, src => new PasswordProfile()
               {
                   Password = src.Password,
                   EnforceChangePasswordPolicy = false,
                   ForceChangePasswordNextLogin = false
               })
               .Map(dest => dest.AccountEnabled, src => src.IsActive)
               .Map(dest => dest.CreationType, src => "LocalAccount")
               .Map(dest => dest.PasswordPolicies, src => PasswordPolicy.DisablePasswordExpirationAndStrong)
               .Ignore(dest => dest.Email);

        }

        private static string GetSignInNameValue(SignInName name) => name?.Value;

        private static string ReplaceNullOrEmpty(string val) => string.IsNullOrWhiteSpace(val) ? "DEFAULT" : val;
    }
}
