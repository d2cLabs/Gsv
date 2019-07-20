using System.Threading.Tasks;
using Gsv.Configuration.Dto;

namespace Gsv.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
