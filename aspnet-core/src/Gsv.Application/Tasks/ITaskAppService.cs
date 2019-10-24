using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gsv.Objects;
using Gsv.Objects.Dto;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        string GetTodayString();
        List<ShelfDto> GetObjectShelves(int objectId, int categoryId);
        
        Task<List<AllotDto>> GetAllotsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId);   
        Task<List<InStockDto>> GetInStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId);   
        Task<List<OutStockDto>> GetOutStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId);   
        Task<PagedResultDto<InspectDto>> GetInspectsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input);   
        Task<PagedResultDto<StocktakingDto>> GetStocktakingsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input);   

        Task SubmitStocktaking(int id);
        Task DeleteStocktaking(int id);
        Task DeleteInStock(int id);
        Task DeleteAllot(int id);

        #region For Weixin 
        Task<List<AllotDto>> GetWxAllotsAsync(int placeId, int categoryId);
        Task<List<InStockDto>> GetWxInStocksAsync(int placeId, int categoryId);
        Task<List<OutStockDto>> GetWxOutStocksAsync(int placeId, int categoryId);
        Task<List<InspectDto>> GetWxInspectsAsync(int placeId, int categoryId);
        Task<List<StocktakingDto>> GetWxStocktakingsAsync(int placeId, int categoryId);

        void InsertAllot(int fromShelfId, int toShelfId, double quantity, int workerId);
        void InsertInStock(int shelfId, int sourceId, double quantity, int workerId);
        void InsertOutStock(int shelfId, double quantity, int workerId);
        void InsertInspect(int shelfId, float purity, string remark, int workerId);
        void InsertStocktaking(int shelfId, double inventory, int workerId);
        #endregion 
    }
}
