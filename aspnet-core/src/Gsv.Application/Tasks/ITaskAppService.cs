using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        string GetTodayString();
        
        List<TaskShelfDto> GetObjectShelves(int objectId);
        
        Task<List<AllotDto>> GetAllotsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId);   
        Task<List<InStockDto>> GetInStocksByObjectAsync(int objectId, DateTime carryoutDate, int shelfId);   
        Task<List<OutStockDto>> GetOutStocksByObjectAsync(int objectId, DateTime carryoutDate, int shelfId);   
        Task<PagedResultDto<InspectDto>> GetInspectsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId, PagedAndSortedResultRequestDto input);   
        Task<PagedResultDto<StocktakingDto>> GetStocktakingsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId, PagedAndSortedResultRequestDto input);   

        Task SubmitStocktaking(int id);
        Task DeleteStocktaking(int id);
        Task DeleteInStock(int id);
        Task DeleteAllot(int id);

        #region For Weixin 
        Task<List<AllotDto>> GetWxAllotsAsync(int objectId);
        Task<List<InStockDto>> GetWxInStocksAsync(int objectId);
        Task<List<OutStockDto>> GetWxOutStocksAsync(int objectId);
        Task<List<InspectDto>> GetWxInspectsAsync(int objectId);
        Task<List<StocktakingDto>> GetWxStocktakingsAsync(int objectId);

        void InsertAllot(int objectId, int fromShelfId, int toShelfId, double quantity, string remark, int workerId);
        void InsertInStock(int objectId, int shelfId, int sourceId, double quantity, string remark, int workerId);
        void InsertOutStock(int objectId, int shelfId, double quantity, string remark, int workerId);
        void InsertInspect(int objectId, int shelfId, float purity, string remark, int workerId);
        void InsertStocktaking(int objectId, int shelfId, double inventory, string remark, int workerId);
        #endregion 
    }
}
