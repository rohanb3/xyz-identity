namespace IdentityServiceClient
{
    public class Const
    {
        public class Permissions
        {
            public const string RoleClaimType = "extension_Group";

            public class Device
            {
                public const string PermissionForCreate = "xyzies.devicemanagment.device.create";
                public const string PermissionForUpdate = "xyzies.devicemanagment.device.update";
                public const string PermissionForRead = "xyzies.devicemanagment.device.read";
                public const string PermissionForDelete = "xyzies.devicemanagment.device.delete";

                public const string AdminPermissionForCreate = "xyzies.devicemanagment.device.create.admin";
                public const string AdminPermissionForUpdate = "xyzies.devicemanagment.device.update.admin";
                public const string AdminPermissionForRead = "xyzies.devicemanagment.device.read.admin";
                public const string AdminPermissionForDelete = "xyzies.devicemanagment.device.delete.admin";
            }
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
