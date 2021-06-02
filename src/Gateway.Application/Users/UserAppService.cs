using Abp.Application.Services;
using Abp.Domain.Repositories;
using Gateway.Authorization.Users;
using Gateway.Users.Dto;

namespace Gateway.Users
{
    //AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, UserDto>, IUserAppService
    {
        public UserAppService(
            IRepository<User, long> repository)
            : base(repository)
        {
        }

        public bool IsConnectionAlive() => true;
    }
}
