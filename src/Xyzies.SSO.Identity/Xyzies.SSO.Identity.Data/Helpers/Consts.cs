namespace Xyzies.SSO.Identity.Data.Helpers
{
    public class Consts
    {
        private const string _extensionPropertyTemplate = "extension_dc2c5f0a6f0845c385858b049a55e71e_";
        public const string RoleClaimType = "extension_Group";
        public const string CompanyIdClaimType = "extension_CompanyId";
        public const string BranchIdClaimType = "extension_BranchId";

        public const string RolePropertyName = _extensionPropertyTemplate + "Group";
        public const string RetailerIdPropertyName = _extensionPropertyTemplate + "RetailerId";
        public const string CompanyIdPropertyName = _extensionPropertyTemplate + "CompanyId";
        public const string BranchIdPropertyName = _extensionPropertyTemplate + "BranchId";
        public const string AvatarUrlPropertyName = _extensionPropertyTemplate + "AvatarUrl";
        public const string ManagerIdPropertyName = _extensionPropertyTemplate + "ManagerId";
        public const string PhonePropertyName = _extensionPropertyTemplate + "Phone";

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
        }

        public class Roles
        {
            public const string SalesRep = "SalesRep";
            public const string SuperAdmin = "SuperAdmin";
            public const string RetailerAdmin = "RetailerAdmin";
        }

        public class Cache
        {
            public const string PermissionKey = "Permission";
            public const string ExpirationKey = "Expiration";
            public const string UsersKey = "Users";
        }

        public class Scopes
        {
            public const string AzureAccessScope = "https://ardasdev.onmicrosoft.com/api/user_impersonation";
            public const string Full = "xyzies.sso.identity.full";
            public const string Edit = "xyzies.sso.identity.edit";
            public const string Read = "xyzies.sso.identity.read";
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
        public class GrantTypes
        {
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
        }
    }
}
