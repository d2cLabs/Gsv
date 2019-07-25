using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Gsv.Authorization;
using Gsv.Controllers;
using Gsv.Users;
using Gsv.Users.Dto;
using Abp.Web.Models;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Setup)]
    public class UsersController : GsvControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        public async Task<JsonResult> GridData()
        {
            var output = await _userAppService.GetAll(new PagedUserResultRequestDto());
            return Json( new { rows = output.Items });
        }
    }
}
