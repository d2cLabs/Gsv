using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Gsv.Authorization;
using Gsv.Staffing;

namespace Gsv.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Staffing)]
    public class WorkersController : GsvCrudController<Worker, WorkerDto>
    {
        public WorkersController(IRepository<Worker> repository)
            :base(repository)
        {
        }
	}
}