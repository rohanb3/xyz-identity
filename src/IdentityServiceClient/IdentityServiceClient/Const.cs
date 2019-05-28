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

            public readonly static string PermissionForCreate = "xyzies.devicemanagment.device.create";
            public readonly static string PermissionForUpdate = "xyzies.devicemanagment.device.update";
            public readonly static string PermissionForRead = "xyzies.devicemanagment.device.read";
            public readonly static string PermissionForDelete = "xyzies.devicemanagment.device.delete";

            public readonly static string AdminPermissionForCreate = "xyzies.devicemanagment.device.create.admin";
            public readonly static string AdminPermissionForUpdate = "xyzies.devicemanagment.device.update.admin";
            public readonly static string AdminPermissionForRead = "xyzies.devicemanagment.device.read.admin";
            public readonly static string AdminPermissionForDelete = "xyzies.devicemanagment.device.delete.admin";
        }

    }
}
