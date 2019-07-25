using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Runtime.Session;
using Gsv.Configuration.Dto;

namespace Gsv.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GsvAppServiceBase, IConfigurationAppService
    {
        private AppSettingProvider _settingProvider;
        public ConfigurationAppService(AppSettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }
        
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
        public List<PropertyDto> GetSettingsForTenant()
        {
            int tenantId = AbpSession.TenantId.Value;
            List<PropertyDto> lst = new List<PropertyDto>();
            foreach (SettingDefinition sd in _settingProvider.GetSettingDefinitions(null).Where(sd => sd.Scopes.HasFlag(SettingScopes.Tenant)))
            {
                string v = SettingManager.GetSettingValueForTenant(sd.Name, tenantId);
                lst.Add(new PropertyDto(sd, v));              
            }
            return lst;
        }

        public async Task ChangeSettingsForTenant(List<PropertyDto> settings)
        {
            int tenantId = AbpSession.TenantId.Value;
            foreach(PropertyDto p in settings)
            {
                string name = p.Name.Split(' ')[0];
                await SettingManager.ChangeSettingForTenantAsync(tenantId, name, p.Value);
            }
            
        }
    }
}
