using System.Collections.Generic;
using System.Threading.Tasks;
using Gsv.Configuration.Dto;

namespace Gsv.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
        
        // For TenantSettings
        List<PropertyDto> GetSettingsForTenant();
        Task ChangeSettingsForTenant(List<PropertyDto> settings);
    }
}
