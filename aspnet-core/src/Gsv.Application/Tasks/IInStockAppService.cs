using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    public interface IInStockAppService : IApplicationService
    {
        Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId);
    }
}
