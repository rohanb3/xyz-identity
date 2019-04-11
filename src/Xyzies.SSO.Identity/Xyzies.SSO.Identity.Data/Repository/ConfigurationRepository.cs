using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class ConfigurationRepository : EfCoreBaseRepository<string, Configuration>, IRepository<string, Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
