using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using Gateway.Authorization.Users;
using Gateway.Sessions.Dto;

namespace Gateway.Sessions
{
    public class SessionAppService : GatewayAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                var currentUser = UserManager.GetUserById(AbpSession.UserId.Value);
                if (this.IsConfirmedUser(currentUser))
                {
                    output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
                }
            }

            return output;
        }

        private bool IsConfirmedUser(User user)
        {
            return user.IsEmailConfirmed && string.IsNullOrEmpty(user.EmailConfirmationCode);
        }
    }
}
