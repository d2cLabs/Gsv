using System.Threading.Tasks;
using Abp.Application.Services;
using Gsv.Sessions.Dto;

namespace Gsv.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
