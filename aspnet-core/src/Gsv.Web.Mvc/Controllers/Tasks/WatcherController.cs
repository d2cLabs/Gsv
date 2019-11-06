using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Objects;
using Gsv.Tasks;
using System.Collections.Generic;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Watcher, PermissionNames.Pages_Supervisor)]
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

        public ActionResult Allots()
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
        public async Task<JsonResult> GridDataAllot(int objectId, DateTime carryoutDate, int shelfId)
        {
            var output = await _taskAppService.GetAllotsByObjectAsync(objectId, carryoutDate, shelfId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataInStock(int objectId, DateTime carryoutDate, int shelfId)
        {
            var output = await _taskAppService.GetInStocksByObjectAsync(objectId, carryoutDate, shelfId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataOutStock(int objectId, DateTime carryoutDate, int shelfId)
        {
            var output = await _taskAppService.GetOutStocksByObjectAsync(objectId, carryoutDate, shelfId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataInspect(int objectId, DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetInspectsByObjectAsync(objectId, carryoutDate, shelfId, GetPagedInput());
            return Json( new { total = output.TotalCount, rows = output.Items });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataStocktaking(int objectId, DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetStocktakingsByObjectAsync(objectId, carryoutDate, shelfId, GetPagedInput());
            return Json( new { total = output.TotalCount, rows = output.Items });
        }
	}
}