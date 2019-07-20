using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Gsv.Controllers;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : GsvControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
