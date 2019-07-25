using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Roles;
using Gsv.Roles.Dto;
using Gsv.Web.Models.Roles;
using Abp.Web.Models;
using Gsv.MultiTenancy;
using Gsv.MultiTenancy.Dto;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Host)]
    public class RolesController : GsvControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly IRoleAppService _roleAppService;

        public RolesController(ITenantAppService tenantAppService, IRoleAppService roleAppService)
        {
            _tenantAppService = tenantAppService;
            _roleAppService = roleAppService;
        }

        public async Task<IActionResult> Index()
        {
            var permissions = (await _roleAppService.GetAllPermissions()).Items;           
            return View(permissions);
        }

       [DontWrapResult]
        public async Task<JsonResult> GridData()
        {
            var output = await _tenantAppService.GetAll(new PagedTenantResultRequestDto { MaxResultCount = int.MaxValue }); // Paging not implemented yet
            return Json( new { rows = output.Items });
        }

        [DontWrapResult]
        public async Task<JsonResult> GetTenantRoles(string id)     // where id = tenantName
        {
            var output = await _roleAppService.GetTenantRoles(id);
            return Json( new { rows = output.Items });
        }
    }
}
