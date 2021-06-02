using System.Threading.Tasks;
using Abp.Application.Services;
using Gateway.Authorization.Accounts.Dto;

namespace Gateway.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
