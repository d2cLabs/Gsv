using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Tasks;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Supervisor)]
    public class SupervisorController : GsvControllerBase
    {
        private ITaskAppService _taskAppService;
        public SupervisorController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public ActionResult InStocks()
        {
            return View();
        }
        public ActionResult OutStocks()
        {
            return View();
        }
        public ActionResult Inspects()
        {
            return View();
        }
        public ActionResult Stocktaking()
        {
            return View();
        }
	}
}