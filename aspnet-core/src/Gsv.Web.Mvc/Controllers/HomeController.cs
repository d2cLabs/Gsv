using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Gsv.Controllers;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : GsvControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
