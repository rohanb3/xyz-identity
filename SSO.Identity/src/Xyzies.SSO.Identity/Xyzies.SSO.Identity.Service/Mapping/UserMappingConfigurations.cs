﻿using Mapster;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Service.Models.User;

namespace Xyzies.SSO.Identity.Service.Mapping
{
    public static class UserMappingConfigurations
    {
        private static string GetSignInNameValue(SignInName name)
        {
            return name?.Value;
        }

        public static void ConfigureUserMappers()
        {
            //Scan for our mappings
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            TypeAdapterConfig<AzureUser, Profile>.NewConfig()
                .Map(dest => dest.Email, src => GetSignInNameValue(src.SignInNames.FirstOrDefault(signInName => signInName.Type == "emailAddress")))
                .Map(dest => dest.UserName, src => GetSignInNameValue(src.SignInNames.FirstOrDefault(signInName => signInName.Type == "userName")));
        }
    }
}
