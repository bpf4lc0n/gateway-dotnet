using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Gateway.Authorization;

namespace Gateway
{
    [DependsOn(
        typeof(GatewayCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class GatewayApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GatewayAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(GatewayApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
