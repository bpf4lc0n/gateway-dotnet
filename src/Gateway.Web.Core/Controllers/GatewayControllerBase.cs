using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Gateway.Controllers
{
    public abstract class GatewayControllerBase: AbpController
    {
        protected GatewayControllerBase()
        {
            LocalizationSourceName = GatewayConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
