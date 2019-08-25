using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Gsv.Objects;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        string GetTodayString();
        List<PlaceShelf> GetObjectShelves(int objectId, int categoryId);
        
        Task<List<InStockDto>> GetInStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId);   

        Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId);
    }
}
