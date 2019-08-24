using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Gsv.Configuration;
using Gsv.Controllers;
using Gsv.Web.Models.Weixin;
using Gsv.Tasks;

using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.Entities;

namespace Gsv.Web.Controllers
{

    // Base
    [IgnoreAntiforgeryToken]
    public class WeixinAccountController : GsvControllerBase
    {
        public TaskManager TaskManager { get; set; }
        private readonly string _secret;
        private readonly string _agentId;
        private readonly string _corpId;

        public WeixinAccountController(IHostingEnvironment env)
        {
            var appConfiguration = env.GetAppConfiguration();
            _secret = appConfiguration[string.Format("SenparcWeixinSetting:{0}:Secret", "App01")];
            _agentId = appConfiguration[string.Format("SenparcWeixinSetting:{0}:AgentId", "App01")];
            _corpId = appConfiguration["SenparcWeixinSetting:CorpId"];
        }

        public ActionResult Login(string returnUrl) 
        {
            var vm = new LoginViewModel() {
                Objects = new List<LoginObject>(),
                ReturnUrl = returnUrl
            };

            vm.WorkerCn = "60001";
            var worker = TaskManager.GetWorkerByCn(vm.WorkerCn);
            if (worker == null || string.IsNullOrEmpty(worker.PlaceList))
            {
                ModelState.AddModelError("", "员工信息错误，请重新输入");
                return View(vm);
            }

            List<LoginObject> loginObjects = new List<LoginObject>();
            foreach (var placecn in worker.PlaceList.Split('|', ',')) 
            {
                var place = TaskManager.GetPlaceByCn(placecn);
                if (place == null) {
                    ModelState.AddModelError("", "员工工作场所编码错误");
                    return View(vm);
                }

                foreach (var obj in TaskManager.GetObjectsByPlace(place.Id))
                {
                    loginObjects.Add(new LoginObject() 
                    {
                        ObjectId = obj.Id,
                        PlaceName = place.Name,
                        CategoryName = TaskManager.GetCategory(obj.CategoryId).Name
                    });
                }
            }

            if (loginObjects.Count == 0)
            {
                ModelState.AddModelError("", "没有对应的监视标的");
                return View(vm);
            }
            vm.Objects = loginObjects;
            return View(vm);
        }
        
        public async Task<ActionResult> Logout()
        {
            //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           return RedirectToAction("InList", "Weixin");
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            var claims = new[] 
            { 
                new Claim("Cn", vm.WorkerCn),
                new Claim("ObjectId", vm.ObjectId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);

            Task.Run(async () =>
            {
                //登录用户，相当于ASP.NET中的FormsAuthentication.SetAuthCookie
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    user,
                    new AuthenticationProperties() {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                    });
            }).Wait();

            string action = GetActionOfReturnUrl(vm.ReturnUrl);
            return RedirectToAction(action, "Weixin");
        }

        #region Utils

        private string GetActionOfReturnUrl(string url)
        {
            int i = url.LastIndexOf('/');
            return url.Substring(i + 1, url.Length - i - 1);
        }

        #endregion
    }
}