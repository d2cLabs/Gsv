using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Objects;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Objects)]
    public class CapitalsController : GsvCrudController<Capital, CapitalDto>
    {
        public CapitalsController(IRepository<Capital> repository)
            :base(repository)
        {
        }
	}
}