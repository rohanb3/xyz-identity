namespace IdentityServiceClient
{
    public class Const
    {
        public class Permissions
        {
            public const string RoleClaimType = "extension_Group";
        }
        public class IndentityApi
        {
            public const string UserEntity = "users";
            public const string RoleEntity = "role";
        }
        public class Auth
        {
            public const string AuthHeader = "Authorization";
            public const string BearerToken = "Bearer ";
        }

        public class Scopes
        {
            public const string Full = "xyzies.sso.identity.full";
            public const string Edit = "xyzies.sso.identity.edit";
            public const string Read = "xyzies.sso.identity.read";
        }

    }
}
