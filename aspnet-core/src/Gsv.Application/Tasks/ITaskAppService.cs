using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gsv.Objects;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        string GetTodayString();
        List<Shelf> GetObjectShelves(int objectId, int categoryId);
        
        Task<List<InStockDto>> GetInStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId);   
        Task<List<OutStockDto>> GetOutStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId);   
        Task<PagedResultDto<InspectDto>> GetInspectsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input);   
        Task<PagedResultDto<StocktakingDto>> GetStocktakingsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input);   

        Task SubmitStocktaking(int id);

        #region For Weixin 
        Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId);
        #endregion 
    }
}
