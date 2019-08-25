using Abp.Application.Services.Dto;
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

        protected PagedAndSortedResultRequestDto GetPagedInput()
        {
            PagedAndSortedResultRequestDto input = new PagedAndSortedResultRequestDto();
            input.Sorting = GetSorting();
            input.MaxResultCount = int.Parse(Request.Form["rows"]);
            input.SkipCount = (int.Parse(Request.Form["page"]) - 1) * input.MaxResultCount;
            return input;
        }

        protected string GetSorting()
        {
            var a = Request.Form["sort"];
            var b = Request.Form["order"];
            return $"{Request.Form["sort"]} {Request.Form["order"]}";
        }
    }
}
