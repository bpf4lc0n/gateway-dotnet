using Abp.Authorization;
using Abp.Localization;

namespace Gateway.Authorization
{
    public class GatewayAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_GlobalSettings, L("GlobalSettings"));
            context.CreatePermission(PermissionNames.Func_ChangeApplicationSettings, L("ChangeApplicationSettings"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GatewayConsts.LocalizationSourceName);
        }
    }
}
