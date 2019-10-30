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
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;

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

        public ActionResult Login(string returnUrl, string code) 
        {
            if (string.IsNullOrEmpty(code))
            {
                // return Redirect(OAuth2Api.GetCode(_corpId, AbsoluteUri(), "STATE", _agentId));
            }

            var accessToken = AccessTokenContainer.GetToken(_corpId, _secret);
            //GetUserInfoResult userInfo = OAuth2Api.GetUserId(accessToken, code);
            //Logger.Info(string.Format("accessKey={0} code={1}, corpId={2}, secret={3}", accessToken, code, _corpId, _secret));
            //Logger.Info(userInfo.UserId);

            //var ret = GetObjects(userInfo.UserId);
            //if (ret.Item2 != null) return Content(ret.Item2);

            var vm = new LoginViewModel() {
                //WorkerCn = userInfo.UserId,
                //Password = "123456",
                //Objects = ret.Item1,   // new List<LoginObject>(),
                ReturnUrl = returnUrl
            };

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
            var worker = TaskManager.GetWorkerByCn(vm.WorkerCn);
            if (worker == null || worker.Password != vm.Password)
            {
                ModelState.AddModelError("", "用户名或密码错误");
                return View(vm);
            }
            if (vm.ObjectId == 0)
            {
                var ret = GetObjects(vm.WorkerCn);
                if (ret.Item2 != null) {
                    ModelState.AddModelError("", ret.Item2);
                    return View(vm);
                }
                vm.Objects = ret.Item1;
                return View(vm);
            }

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

        private (List<LoginObject>, string) GetObjects(string workerCn)
        {
            var worker = TaskManager.GetWorkerByCn(workerCn); // userInfo.UserId);
            if (worker == null) return (null, "系统中没有登记此员工");
            if (string.IsNullOrEmpty(worker.PlaceList)) return (null, "此员工没有设置工作场所");
            
            List<LoginObject> loginObjects = new List<LoginObject>();
            foreach (var placecn in worker.PlaceList.Split('|', ',')) 
            {
                var place = TaskManager.GetPlaceByCn(placecn);
                if (place == null) return (null, "员工工作场所中的编码错误");

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

            if (loginObjects.Count == 0) return (null, "没有对应的监视标的");
            return (loginObjects, null);
        }
        #endregion
    }
}