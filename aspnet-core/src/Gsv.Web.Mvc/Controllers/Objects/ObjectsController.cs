using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Objects;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Objects)]
    public class ObjectsController : GsvCrudController<Object, ObjectDto>
    {
        public ObjectsController(IRepository<Object> repository)
            :base(repository)
        {
        }
	}
}