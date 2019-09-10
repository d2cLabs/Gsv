using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Tasks;
using Gsv.Objects;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Supervisor)]
    public class SupervisorController : GsvControllerBase
    {
        private readonly IObjectAppService _objectAppService;
        private readonly ITaskAppService _taskAppService;
        public SupervisorController(IObjectAppService objectAppService, ITaskAppService taskAppService)
        {
            _objectAppService = objectAppService;
            _taskAppService = taskAppService;
        }


        public ActionResult Home()
        {
            return View();
        }

        [DontWrapResult]
        public async Task<JsonResult> GridData()
        {
            var output = await _objectAppService.GetObjectsAsync(GetSorting());
            return Json( new { rows = output });
        }


	}
}