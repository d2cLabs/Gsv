using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Gsv.Configuration;
using Gsv.Controllers;
using Gsv.Web.Models.Weixin;
using System.Collections.Generic;
using System.Linq;
using Gsv.Tasks;
using Gsv.Objects.Dto;
using Gsv.Objects;
using Gsv.Types.Dto;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.Containers;

namespace Gsv.Web.Controllers
{
    // Base
    [IgnoreAntiforgeryToken]
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class WeixinController : GsvControllerBase
    {
        public TaskManager TaskManager { get; set; }
        private readonly string _secret;
        private readonly string _agentId;
        private readonly string _corpId;

        private readonly ITaskAppService _taskAppService;

        public WeixinController(IHostingEnvironment env, ITaskAppService taskAppService)
        {
            var appConfiguration = env.GetAppConfiguration();
            _secret = appConfiguration[string.Format("SenparcWeixinSetting:{0}:Secret", "App01")];
            _agentId = appConfiguration[string.Format("SenparcWeixinSetting:{0}:AgentId", "App01")];
            _corpId = appConfiguration["SenparcWeixinSetting:CorpId"];

            _taskAppService = taskAppService;
        }

        #region InStock

        public ActionResult InList()
        {
            return View(GetInListViewModel());
        }

        public ActionResult InStock()
        {
            var timestamp = JSSDKHelper.GetTimestamp();
            //获取随机码
            var nonceStr = JSSDKHelper.GetNoncestr();
            string ticket = JsApiTicketContainer.GetTicket(_corpId, _secret);
            //获取签名
            var signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, AbsoluteUri());
 
            ViewBag.AppId = _corpId;
            ViewBag.Timestamp = timestamp;
            ViewBag.NonceStr = nonceStr;
            ViewBag.Signature = signature;

            return View(GetInStockViewModel());
        } 

        [HttpPost]
        public ActionResult InsertInStock(InStockViewModel vm)
        {
            _taskAppService.InsertInStock(vm.ShelfId, vm.SourceId, vm.Quantity, GetWorkerId());
            // valid vm ToDO
            return RedirectToAction("InList");
        }

        private ListViewModel GetInListViewModel()
        {
            var obj = GetObject();
            if (obj == null) return null;
            ListViewModel vm = new ListViewModel();
            vm.PlaceInfo = TaskManager.GetObjectPlaceInfo(obj.Id);
            var ret = TaskManager.GetObjectCollateral(obj.Id);
            vm.Collateral = string.Format("类型:{0}   库存:{1:F2}   黄线:{2}", ret.Item1, ret.Item2, ret.Item3);

            var items = _taskAppService.GetWxInStocksAsync(obj.PlaceId, obj.CategoryId).Result;
            vm.Items = new List<ItemInfo>();
            double total = 0.0;
            foreach (var item in items)
            {
                vm.Items.Add(new ItemInfo {
                    CreateTime = item.CreateTime.ToString("HH:mm:ss"),
                    Shelf = item.ShelfName,
                    Quantity = item.Quantity.ToString("F2"),
                    CreateWorker = item.WorkerName,
                });
                total += item.Quantity;
            }
            vm.TodaySummary = string.Format("今日笔数({0})  进库总重({1:F2})", items.Count, total);
        
            return vm;
        }
        private InStockViewModel GetInStockViewModel()
        {
            var obj = GetObject();
            InStockViewModel vm = new InStockViewModel();
            vm.Shelves = ObjectMapper.Map<List<ShelfDto>>(TaskManager.GetObjectShelves(obj.Id, obj.CategoryId));
            vm.Sources = ObjectMapper.Map<List<SourceDto>>(TaskManager.GetSources());
            return vm;
        }
        #endregion 

        #region OutStock

        public ActionResult OutList()
        {
            return View(GetOutListViewModel());
        }

        public ActionResult OutStock()
        {
            return View(GetOutStockViewModel());
        } 

        [HttpPost]
        public ActionResult InsertOutStock(OutStockViewModel vm)
        {
            _taskAppService.InsertOutStock(vm.ShelfId, vm.Quantity, GetWorkerId());
            // valid vm ToDO
            return RedirectToAction("OutList");
        }

        private ListViewModel GetOutListViewModel()
        {
            var obj = GetObject();
            if (obj == null) return null;
            ListViewModel vm = new ListViewModel();
            vm.PlaceInfo = TaskManager.GetObjectPlaceInfo(obj.Id);
            var ret = TaskManager.GetObjectCollateral(obj.Id);
            vm.Collateral = string.Format("类型:{0}   库存:{1:F2}   黄线:{2}", ret.Item1, ret.Item2, ret.Item3);

            var items = _taskAppService.GetWxOutStocksAsync(obj.PlaceId, obj.CategoryId).Result;
            vm.Items = new List<ItemInfo>();
            double total = 0.0;
            foreach (var item in items)
            {
                vm.Items.Add(new ItemInfo {
                    CreateTime = item.CreateTime.ToString("HH:mm:ss"),
                    Shelf = item.ShelfName,
                    Quantity = item.Quantity.ToString("F2"),
                    CreateWorker = item.WorkerName,
                });
                total += item.Quantity;
            }
            vm.TodaySummary = string.Format("今日笔数({0})  出库总重({1:F2})  余量({2:F2})", items.Count, total, ret.Item2 - ret.Item3);
        
            return vm;
        }
        private OutStockViewModel GetOutStockViewModel()
        {
            var obj = GetObject();
            OutStockViewModel vm = new OutStockViewModel();
            vm.Shelves = ObjectMapper.Map<List<ShelfDto>>(TaskManager.GetObjectShelves(obj.Id, obj.CategoryId));
            return vm;
        }
        #endregion 

        #region Inspect

        public ActionResult InspectList()
        {
            return View(GetInspectListViewModel());
        }

        public ActionResult Inspect()
        {
            return View(GetInspectViewModel());
        } 

        [HttpPost]
        public ActionResult InsertInspect(InspectViewModel vm)
        {
            _taskAppService.InsertInspect(vm.ShelfId, vm.Purity, vm.Remark, GetWorkerId());
            // valid vm ToDO
            return RedirectToAction("InspectList");
        }

        private ListViewModel GetInspectListViewModel()
        {
            var obj = GetObject();
            if (obj == null) return null;
            ListViewModel vm = new ListViewModel();
            vm.PlaceInfo = TaskManager.GetObjectPlaceInfo(obj.Id);
            var ret = TaskManager.GetObjectCollateral(obj.Id);
            vm.Collateral = string.Format("类型:{0}   库存:{1:F2}   黄线:{2}", ret.Item1, ret.Item2, ret.Item3);

            var items = _taskAppService.GetWxInspectsAsync(obj.PlaceId, obj.CategoryId).Result;
            vm.Items = new List<ItemInfo>();
            double total = 0.0;
            foreach (var item in items)
            {
                vm.Items.Add(new ItemInfo {
                    CreateTime = item.CreateTime.ToString("HH:mm:ss"),
                    Shelf = item.ShelfName,
                    Quantity = item.Purity.ToString("F2"),
                    CreateWorker = item.WorkerName,
                });
                total += item.Purity;
            }
            vm.TodaySummary = string.Format("今日笔数({0})  平均纯度({1:F2})", items.Count, total/items.Count);
        
            return vm;
        }
        private InspectViewModel GetInspectViewModel()
        {
            var obj = GetObject();
            InspectViewModel vm = new InspectViewModel();
            vm.Shelves = ObjectMapper.Map<List<ShelfDto>>(TaskManager.GetObjectShelves(obj.Id, obj.CategoryId));
            return vm;
        }
        #endregion 

        #region Stocktaking

        public ActionResult StocktakingList()
        {
            return View(GetStocktakingListViewModel());
        }

        public ActionResult Stocktaking()
        {
            return View(GetStocktakingViewModel());
        } 

        [HttpPost]
        public ActionResult InsertStocktaking(StocktakingViewModel vm)
        {
            _taskAppService.InsertStocktaking(vm.ShelfId, vm.Inventory, GetWorkerId());
            // valid vm ToDO
            return RedirectToAction("StocktakingList");
        }

        private ListViewModel GetStocktakingListViewModel()
        {
            var obj = GetObject();
            if (obj == null) return null;
            ListViewModel vm = new ListViewModel();
            vm.PlaceInfo = TaskManager.GetObjectPlaceInfo(obj.Id);
            var ret = TaskManager.GetObjectCollateral(obj.Id);
            vm.Collateral = string.Format("类型:{0}   库存:{1:F2}   黄线:{2}", ret.Item1, ret.Item2, ret.Item3);

            var items = _taskAppService.GetWxStocktakingsAsync(obj.PlaceId, obj.CategoryId).Result;
            vm.Items = new List<ItemInfo>();
            double total = 0.0;
            foreach (var item in items)
            {
                vm.Items.Add(new ItemInfo {
                    CreateTime = item.CreateTime.ToString("HH:mm:ss"),
                    Shelf = item.ShelfName,
                    Quantity = item.Inventory.ToString("F2"),
                    CreateWorker = item.WorkerName,
                });
                total += item.Inventory;
            }
            vm.TodaySummary = string.Format("今日笔数({0})  总称重({1:F2})", items.Count, total);
        
            return vm;
        }
        private StocktakingViewModel GetStocktakingViewModel()
        {
            var obj = GetObject();
            StocktakingViewModel vm = new StocktakingViewModel();
            vm.Shelves = ObjectMapper.Map<List<ShelfDto>>(TaskManager.GetObjectShelves(obj.Id, obj.CategoryId));
            return vm;
        }
        #endregion 

        #region Common Utils

        private Object GetObject()
        {
            var claim = HttpContext.User.Claims.First(x => x.Type == "ObjectId");
            int id = int.Parse(claim.Value);
            if (id == 0) return null;
            return TaskManager.GetObject(id);
        }
        private int GetWorkerId()
        {
            var claim = HttpContext.User.Claims.First(x => x.Type == "Cn");
            return TaskManager.GetWorkerByCn(claim.Value).Id;
        }
        #endregion

    }
}