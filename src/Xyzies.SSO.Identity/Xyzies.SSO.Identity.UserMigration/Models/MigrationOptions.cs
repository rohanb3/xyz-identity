namespace Xyzies.SSO.Identity.UserMigration.Models
{
    public class MigrationOptions
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public string[] Emails { get; set; }
    }
}
