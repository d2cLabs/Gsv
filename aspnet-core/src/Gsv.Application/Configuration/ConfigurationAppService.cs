using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Gsv.Configuration.Dto;

namespace Gsv.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GsvAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
