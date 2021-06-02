using System.Threading.Tasks;
using Abp.Authorization;
using Gateway.Authorization;
using Gateway.Configuration.Dto;

namespace Gateway.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GatewayAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeGlobalSettings(GlobalSettingsInput input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.AppName, input.AppName);
        }
    }
}
