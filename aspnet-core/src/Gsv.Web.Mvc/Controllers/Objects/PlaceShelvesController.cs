using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Objects;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Objects)]
    public class PlaceShelvesController : GsvCrudController<PlaceShelf, PlaceShelfDto>
    {
        public PlaceShelvesController(IRepository<PlaceShelf> repository)
            :base(repository)
        {
        }
	}
}