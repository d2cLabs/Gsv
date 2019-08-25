using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Gsv.Objects;
using Gsv.Tasks.Dto;

namespace Gsv.Tasks
{
    [AbpAuthorize]
    public class TaskAppService : GsvAppServiceBase, ITaskAppService
    {
        public TaskManager TaskManager { get; set; }
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private readonly IRepository<InStock> _inStockRepository;
        private readonly IRepository<OutStock> _outStockRepository;
        private readonly IRepository<Inspect> _inspectRepository;
        private readonly IRepository<Stocktaking> _stocktakingRepository;

        public TaskAppService(IRepository<InStock> inStockRepository,
            IRepository<OutStock> outStockRepository,
            IRepository<Inspect> inspectRepository,
            IRepository<Stocktaking> stocktakingRepository)
        {
            _inStockRepository = inStockRepository;
            _outStockRepository = outStockRepository;
            _inspectRepository = inspectRepository;
            _stocktakingRepository = stocktakingRepository;
        }

        public string GetTodayString()
        {
            return DateTime.Today.ToString("yyyy-MM-dd");
        }
        
        public List<PlaceShelf> GetObjectShelves(int objectId, int categoryId)
        {
            return TaskManager.GetObjectShelves(objectId, categoryId);
        }
                
        public async Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.PlaceShelf, x=> x.PlaceShelf.CargoType, x => x.CreateWorker);
            query = query.Where(x => x.CarryoutDate == DateTime.Today && x.PlaceShelf.PlaceId == placeId && x.PlaceShelf.CargoType.CategoryId == categoryId);
            
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }
                
        public async Task<List<InStockDto>> GetInStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.PlaceShelf, x => x.Source, x => x.CreateWorker);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.ShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.PlaceShelf.PlaceId == placeId);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }
                
    }

}