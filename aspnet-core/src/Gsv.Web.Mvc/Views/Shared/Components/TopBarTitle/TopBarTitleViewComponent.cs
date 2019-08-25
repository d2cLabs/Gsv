using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Configuration;
using Abp.Runtime.Session;
using Gsv.Configuration;
using Abp.UI;
using Gsv.Authorization.Users;

namespace Gsv.Web.Views.Shared.Components.TopBarTitle
{
    public class TopBarTitleViewComponent : GsvViewComponent
    {
        private const string Images_DIR = "~/images/";
        private readonly ISettingManager _settingManager;
        private readonly IAbpSession _abpSession;
        private readonly UserManager _userManager;

        public TopBarTitleViewComponent(ISettingManager settingManager, IAbpSession abpSession, UserManager userManager)
        {
            _settingManager = settingManager;
            _abpSession = abpSession;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name = await _settingManager.GetSettingValueAsync(AppSettingNames.VI.CompanyImageName);
            var model = new TopBarTitleViewModel
            {
                CompanyImageName = Images_DIR + name,
                CompanyName = await _settingManager.GetSettingValueAsync(AppSettingNames.VI.CompanyName),
                AppName = AppConsts.AppName,
                UserName = await GetUserTitile()
            };

            return View(model);
        }

        private async Task<string> GetUserTitile() 
        {
            var user = await _userManager.GetUserByIdAsync(_abpSession.UserId??0);

            return string.Format("{0} {1}", user.UserName, user.Name);
        }
    }
}
