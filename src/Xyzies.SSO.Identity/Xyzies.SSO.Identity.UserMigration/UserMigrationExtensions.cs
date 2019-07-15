using Microsoft.Extensions.DependencyInjection;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.UserMigration.Services.Migrations;

namespace Xyzies.SSO.Identity.UserMigration
{
    public static class UserMigrationExtensions
    {
        public static void AddUserMigrationService(this IServiceCollection services)
        {
            services.AddScoped<ICpUsersRepository, CpUsersRepository>();
            services.AddScoped<IAzureAdClient, AzureAdClient>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserMigrationHistoryRepository, UserMigrationHistoryRepository>();

            services.AddScoped<IMigrationService, MigrationService>();
        }
    }
}