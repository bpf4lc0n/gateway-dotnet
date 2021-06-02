using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Gateway.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            roleManagementConfig.StaticRoles.Add(
               new StaticRoleDefinition(
                   StaticRoleNames.Tenants.SuperAdmin,
                   MultiTenancySides.Tenant
               )
           );
        }
    }
}
