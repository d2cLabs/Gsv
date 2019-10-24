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
        public async Task<JsonResult> GridDataAllot(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetAllotsByDateAndShelfAsync(carryoutDate, shelfId, placeId, categoryId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataInStock(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetInStocksByDateAndShelfAsync(carryoutDate, shelfId, placeId, categoryId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataOutStock(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetOutStocksByDateAndShelfAsync(carryoutDate, shelfId, placeId, categoryId);
            return Json( new { rows = output });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataInspect(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetInspectsByDateAndShelfAsync(carryoutDate, shelfId, placeId, categoryId, GetPagedInput());
            return Json( new { total = output.TotalCount, rows = output.Items });
        }

        [DontWrapResult]
        public async Task<JsonResult> GridDataStocktaking(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var output = await _taskAppService.GetStocktakingsByDateAndShelfAsync(carryoutDate, shelfId, placeId, categoryId, GetPagedInput());
            return Json( new { total = output.TotalCount, rows = output.Items });
        }
	}
}