using System.ComponentModel;

namespace Xyzies.SSO.Identity.CPUserMigration.Models
{
    public class MigrationSchedulerOptions
    {
        [DefaultValue(3600)]
        public double Period { get; set; }
        [DefaultValue(500)]
        public int UsersLimit { get; set; }
        [DefaultValue(true)]
        public bool Enabled { get; set; }
    }
}
