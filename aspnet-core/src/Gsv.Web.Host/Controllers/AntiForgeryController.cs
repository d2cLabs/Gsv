using Microsoft.AspNetCore.Antiforgery;
using Gsv.Controllers;

namespace Gsv.Web.Host.Controllers
{
    public class AntiForgeryController : GsvControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
