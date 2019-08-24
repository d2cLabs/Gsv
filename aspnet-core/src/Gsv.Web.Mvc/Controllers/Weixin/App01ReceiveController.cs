using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Gsv.Web.Controllers
{
    [IgnoreAntiforgeryToken]
    public class App01ReceiveController : WorkReceiveControllerBase
    {
        public App01ReceiveController(IHostingEnvironment env)
            :base(env, "App01")
        {
        }
    }
}