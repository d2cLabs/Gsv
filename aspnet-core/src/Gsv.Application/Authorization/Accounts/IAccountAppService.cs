using System.Threading.Tasks;
using Abp.Application.Services;
using Gsv.Authorization.Accounts.Dto;

namespace Gsv.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
