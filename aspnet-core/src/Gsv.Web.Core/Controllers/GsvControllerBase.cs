using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Gsv.Controllers
{
    public abstract class GsvControllerBase: AbpController
    {
        protected GsvControllerBase()
        {
            LocalizationSourceName = GsvConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
