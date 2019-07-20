using Abp.AspNetCore.Mvc.ViewComponents;

namespace Gsv.Web.Views
{
    public abstract class GsvViewComponent : AbpViewComponent
    {
        protected GsvViewComponent()
        {
            LocalizationSourceName = GsvConsts.LocalizationSourceName;
        }
    }
}
