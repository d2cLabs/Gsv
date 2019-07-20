using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace Gsv.Web.Views
{
    public abstract class GsvRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected GsvRazorPage()
        {
            LocalizationSourceName = GsvConsts.LocalizationSourceName;
        }
    }
}
