using Abp.Application.Services;
using Gateway.Users.Dto;

namespace Gateway.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, UserDto>
    {
        bool IsConnectionAlive();
    }
}
