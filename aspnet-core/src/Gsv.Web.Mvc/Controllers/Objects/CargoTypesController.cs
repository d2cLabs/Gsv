using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Objects;
using Gsv.Objects.Dto;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Objects)]
    public class CargoTypesController : GsvCrudController<CargoType, CargoTypeDto>
    {
        public CargoTypesController(IRepository<CargoType> repository)
            :base(repository)
        {
        }
	}
}