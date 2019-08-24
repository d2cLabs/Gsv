using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Types;
using Gsv.Types.Dto;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Types)]
    public class SourcesController : GsvCrudController<Source, SourceDto>
    {
        public SourcesController(IRepository<Source> repository)
            :base(repository)
        {
        }
	}
}