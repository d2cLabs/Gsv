using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Configuration;
using Abp.Web.Models;
using Gsv.Authorization;
using Gsv.Configuration;
using Gsv.Controllers;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Setup)]
    public class TenantSettingsController : GsvControllerBase
    {
        private readonly IConfigurationAppService _configurationAppService;
        private readonly AppSettingProvider _settingProvider;   
        public TenantSettingsController(IConfigurationAppService configurationAppService, AppSettingProvider settingProvider) 
        {
            _configurationAppService = configurationAppService;
            _settingProvider = settingProvider;
        }

        public ActionResult Index()
        {
            var settings = _settingProvider.GetSettingDefinitions(null).Where(sd => sd.Scopes.HasFlag(SettingScopes.Tenant));

            return View(settings);
        }

        [DontWrapResult]
        public JsonResult GridData()
        {
            var settings = _configurationAppService.GetSettingsForTenant();
            return Json(new {total=settings.Count(), rows=settings}); 
        }
	}
}