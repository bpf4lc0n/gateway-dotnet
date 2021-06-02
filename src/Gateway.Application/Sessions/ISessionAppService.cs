using System.Threading.Tasks;
using Abp.Application.Services;
using Gateway.Sessions.Dto;

namespace Gateway.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
