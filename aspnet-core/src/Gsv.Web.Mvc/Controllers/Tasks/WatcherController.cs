using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Objects;
using Gsv.Tasks;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Watcher)]
    public class WatcherController : GsvControllerBase
    {
        private readonly IObjectAppService _objectAppService;
        private readonly ITaskAppService _taskAppService;
        public WatcherController(IObjectAppService objectAppService, ITaskAppService taskAppService)
        {
            _objectAppService = objectAppService;
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

        [DontWrapResult]
        public async Task<JsonResult> GridData()
        {
            var output = await _objectAppService.GetObjectsAsync(GetSorting());
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataInStock(DateTime carryoutDate, int shelfId, int placeId)
        {
            var output = await _taskAppService.GetInStocksByDateAndShelfAsync(carryoutDate, shelfId, placeId);
            return Json( new { rows = output });
        }

	}
}