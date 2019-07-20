using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gsv.MultiTenancy.Dto;

namespace Gsv.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

