﻿using System;
using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Data.Helpers
{
    /// <summary>
    /// NOTE: Do not change constant values without full recompilation
    /// </summary>
    /// 
    public class Consts
    {
        /// <summary>
        /// The following code is needed to replace the constant value, depends on appsettings.json.
        /// For local development, uncomment the first line and comment out the second.
        /// 
        /// Template to replace is 'variable58b049a55e71e'
        /// </summary>
        // private const string _extensionPropertyTemplate = "extension_18af08da906c46e1bee10f094fd0e035_";//
        private const string _extensionPropertyTemplate = "extension_variable58b049a55e71e_";

        public const string RoleClaimType = "extension_Group";
        public const string CompanyIdClaimType = "extension_CompanyId";
        public const string BranchIdClaimType = "extension_BranchId";

        public const string RolePropertyName = _extensionPropertyTemplate + "Group";
        public const string DepartmentIdPropertyName = _extensionPropertyTemplate + "DepartmentId";
        public const string RetailerIdPropertyName = _extensionPropertyTemplate + "RetailerId";
        public const string CompanyIdPropertyName = _extensionPropertyTemplate + "CompanyId";
        public const string BranchIdPropertyName = _extensionPropertyTemplate + "BranchId";
        public const string AvatarUrlPropertyName = _extensionPropertyTemplate + "AvatarUrl";
        public const string ManagerIdPropertyName = _extensionPropertyTemplate + "ManagerId";
        public const string CPUserIdPropertyName = _extensionPropertyTemplate + "CPUserId";
        public const string PhonePropertyName = _extensionPropertyTemplate + "Phone";
        public const string StatusIdPropertyName = _extensionPropertyTemplate + "StatusId";

        public const string CityPropertyName = "City";
        public const string StatePropertyName = "State";
        public const string UserNamePropertyName = "displayName ";

        public static string UserIdPropertyName = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        public class GraphApi
        {
            public const string GraphApiEndpoint = "https://graph.windows.net/";
            public const string ApiVersionParameter = "api-version";
            public const string ApiVersion = "1.6";
            public const string UserEntity = "users";
            public const string ObjectUserEntity = "directoryObjects/$/Microsoft.DirectoryServices.User";

            public class Errors
            {
                public const string UserAlreadyExist = "Another object with the same value for property signInNames already exists.";
            }
        }

        public class Roles
        {
            public const string SalesRep = "salesrep";
            public const string SupportAdmin = "supportadmin";
            public const string SuperAdmin = "superadmin";

            [Obsolete("Only for migration, delete in next version")]
            public const string Operator = "operator";

            public const string OperationsAdmin = "operationadmin";
            public const string SystemAdmin = "systemadmin";
            public const string AccountAdmin = "accountadmin";
            public static readonly List<string> GlobalAdmins = new List<string>
            {
                SystemAdmin,
                AccountAdmin,
                OperationsAdmin
            };
        }

        public class Cache
        {
            public const string PermissionKey = "Permission";
            public const string ExpirationKey = "Expiration";
            public const string UsersKey = "Users";
        }

        public class UsersSorting
        {
            public const string Id = "userid";
            public const string Name = "username";
            public const string Role = "role";
            public const string State = "state";
            public const string City = "city";
            public const string Descending = "desc";
            public const string Ascending = "asc";
        }

        public class UserStatuses
        {
            public const string Active = "active";
            public const string Disabled = "disabled";

            public const string CPActiveUserStatus = "approved";

            public static string[] Statuses = { Active, Disabled };
        }

        public class GrantTypes
        {
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
        }
        public class PasswordPolicy
        {
            public const string DisablePasswordExpirationAndStrong = "DisablePasswordExpiration, DisableStrongPassword";
            public const string DisablePasswordExpiration = "DisablePasswordExpiration";
            public const string DisablePasswordString = "DisableStrongPassword";
        }

        public class Security
        {
            public const string StaticToken = "d64ded6fd8db3fead6c90e600d85cccc02cd3e2dafcc29e8d1ade61263229d0b16b5a92ffa1bad1d6325e302461c7a69630c5c913ab47fb7e284dcabba1ac91e";
        }

        public class ConfigurationKeys
        {
            public const string HTMLCodeEmailTemplate = "HTMLCodeEmailTemplate";
            public const string TextCodeEmailTemplate = "TextCodeEmailTemplate";
        }

        public class ErrorReponses
        {
            public const string CodeIsNotValid = "Code is not valid";
            public const string UserDoesNotExits = "User does not exist";
            public const string AzureLoginError = "The username or password provided in the request are invalid";
        }

        public class UsersReadPermission
        {
            public const string ReadAll = "xyzies.identity.user.read.all";
            public const string ReadInCompany = "xyzies.identity.user.read.incompany";
            public const string ReadOnlyRequester = "xyzies.identity.user.read.myself";

        }
    }
}
