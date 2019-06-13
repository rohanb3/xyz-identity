namespace IdentityServiceClient
{
    public class Const
    {
        public class Permissions
        {
            public const string RoleClaimType = "extension_Group";

            public class Device
            {
                public const string Create = "xyzies.devicemanagment.device.create";
                public const string Update = "xyzies.devicemanagment.device.update";
                public const string Read = "xyzies.devicemanagment.device.read";
                public const string Delete = "xyzies.devicemanagment.device.delete";

                public const string AdminCreate = "xyzies.devicemanagment.device.create.admin";
                public const string AdminUpdate = "xyzies.devicemanagment.device.update.admin";
                public const string AdminRead = "xyzies.devicemanagment.device.read.admin";
                public const string AdminDelete = "xyzies.devicemanagment.device.delete.admin";
            }

            public class Comment
            {
                public const string Create = "xyzies.devicemanagment.comment.create";
                public const string Read = "xyzies.devicemanagment.comment.read";
            }

            public class History
            {
                public const string Read = "xyzies.devicemanagment.history.read";
                public const string AdminRead = "xyzies.devicemanagment.history.read.admin";
            }

            public class Dispute
            {
                public const string Create = "xyzies.reconciliation.dispute.create";
                public const string Delete = "xyzies.reconciliation.dispute.delete";
                public const string Read = "xyzies.reconciliation.dispute.read";
                public const string Update = "xyzies.reconciliation.dispute.update";
                public const string Patch = "xyzies.reconciliation.dispute.patch";
                public const string SamPatch = "xyzies.reconciliation.dispute.patch.sam";
                public const string ResubmissiontableRead = "xyzies.reconciliation.web.resubmissiontable.read";
            }

            public class Order
            {
                public const string Read = "xyzies.reconciliation.web.order.read";
                public const string AdminRead = "xyzies.reconciliation.web.order.read.systemadmin";
            }

            public class NotificationEmail
            {
                public const string Create = "xyzies.notification.email.create";
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
