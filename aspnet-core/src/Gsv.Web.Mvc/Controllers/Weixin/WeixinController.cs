using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Gsv.Configuration;
using Gsv.Controllers;
using Gsv.Web.MessageHandlers;

using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.Entities;
using Gsv.Web.Models.Weixin;
using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using Gsv.Tasks;
using Gsv.Types;

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

        private readonly IInStockAppService _inStockAppService;

        public WeixinController(IHostingEnvironment env,
            IInStockAppService inStockAppService)
        {
            var appConfiguration = env.GetAppConfiguration();
            _secret = appConfiguration[string.Format("SenparcWeixinSetting:{0}:Secret", "App01")];
            _agentId = appConfiguration[string.Format("SenparcWeixinSetting:{0}:AgentId", "App01")];
            _corpId = appConfiguration["SenparcWeixinSetting:CorpId"];

            _inStockAppService = inStockAppService;
        }

        #region InReceipt 

        public ActionResult InList()
        {
            return View(GetInListViewModel());
        }

        public ActionResult InBill()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult InsertInBill()
        {
            return RedirectToAction("InList");
        }
        #endregion

        #region Utils

        private int GetObjectId()
        {
            var claim = HttpContext.User.Claims.First(x => x.Type == "ObjectID");
            return int.Parse(claim.Value);
        }

        private ListViewModel GetInListViewModel()
        {
            var obj = TaskManager.GetObject(GetObjectId());
            ListViewModel vm = new ListViewModel();
            vm.PlaceInfo = TaskManager.GetObjectPlaceInfo(obj.Id);
            vm.Collateral = TaskManager.GetObjectCollateral(obj.Id);
            vm.Shelves = TaskManager.GetObjectShelves(obj.Id);

            var items = _inStockAppService.GetInStocksAsync(obj.PlaceId, obj.CategoryId).Result;
            vm.Items = new List<ItemInfo>();
            double total = 0.0;
            foreach (var item in items)
            {
                vm.Items.Add(new ItemInfo {
                    CreateTime = item.CreateTime.ToString("HH:mm:ss"),
                    Shelf = item.PlaceShelfName,
                    Quantity = item.Quantity.ToString("F2"),
                    CreateWorker = item.WorkerName,
                });
                total += item.Quantity;
            }
            vm.TodaySummary = string.Format("今日笔数({0})  进库总重({1:F2})", items.Count, total);
        
            return vm;
        }

        #endregion 
    }
}