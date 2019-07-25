using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Domain.Repositories;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Gsv.Authorization;
using Gsv.MultiTenancy;
using Gsv.MultiTenancy.Dto;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Host)]
    public class TenantsController : GsvCrudController<Tenant, int, TenantDto>
    {
        private readonly ITenantAppService _tenantAppService;
        public TenantsController(IRepository<Tenant> repository, ITenantAppService tenantAppService)
            : base(repository)
        {
            _tenantAppService = tenantAppService;
        }
        
        [HttpPost]
        public async Task<JsonResult> MyUpdate(TenantDto input)
        {
            try
            {
                input.IsActive = true;
                await _tenantAppService.Update(input);
                return Json(new { result = "success", content = "记录修改成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> MyCreate(CreateTenantDto input)
        {
            try
            {
                input.IsActive = true;
                var output = await _tenantAppService.Create(input);
                return Json(new { result = "success", content = "记录修改成功" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("表操作失败", ex.Message);
            }
        }
    }
}
