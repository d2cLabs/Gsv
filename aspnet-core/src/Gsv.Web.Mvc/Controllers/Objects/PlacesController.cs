using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Objects;
using Gsv.Objects.Dto;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Objects)]
    public class PlacesController : GsvCrudController<Place, PlaceDto>
    {
        public PlacesController(IRepository<Place> repository)
            :base(repository)
        {
        }
	}
}