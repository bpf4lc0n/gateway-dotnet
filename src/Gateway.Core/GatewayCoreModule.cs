using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Gateway.Authorization.Roles;
using Gateway.Authorization.Users;
using Gateway.Configuration;
using Gateway.Localization;
using Gateway.MultiTenancy;
using Gateway.Timing;

namespace Gateway
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class GatewayCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            GatewayLocalizationConfigurer.Configure(Configuration.Localization);
            Configuration.MultiTenancy.IsEnabled = GatewayConsts.MultiTenancyEnabled;
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GatewayCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
