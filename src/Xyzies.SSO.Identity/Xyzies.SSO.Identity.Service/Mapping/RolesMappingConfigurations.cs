using Mapster;
using Xyzies.SSO.Identity.Services.Models.Permissions;

namespace Xyzies.SSO.Identity.Services.Mapping
{
    public class RolesMappingConfigurations
    {
        public static void ConfigureRoleMappers()
        {
            TypeAdapterConfig<Data.Entity.Role, RoleModel>.NewConfig()
                .Map(dest => dest.IsCustomRole, src => src.IsCustom)
                .Map(dest => dest.RoleKey, src => src.Id);

            TypeAdapterConfig<Data.Entity.Policy, PolicyModel>.NewConfig()
               .Map(dest => dest.PolicyId, src => src.Id)
               .Map(dest => dest.PolicyName, src => src.Name)
               .Map(dest => dest.Scopes, src => src.Permissions);

            TypeAdapterConfig<Data.Entity.Permission, ScopeModel>.NewConfig()
               .Map(dest => dest.ScopeId, src => src.Id)
               .Map(dest => dest.ScopeName, src => src.Scope);
        }
    }
}
