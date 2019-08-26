using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
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

        private readonly IRepository<Shelf> _shelfRepository;

        public TaskAppService(IRepository<InStock> inStockRepository,
            IRepository<OutStock> outStockRepository,
            IRepository<Inspect> inspectRepository,
            IRepository<Stocktaking> stocktakingRepository, 
            IRepository<Shelf> shelfRepository)
        {
            _inStockRepository = inStockRepository;
            _outStockRepository = outStockRepository;
            _inspectRepository = inspectRepository;
            _stocktakingRepository = stocktakingRepository;
            _shelfRepository = shelfRepository;
        }

        public string GetTodayString()
        {
            return DateTime.Today.ToString("yyyy-MM-dd");
        }
        
        public List<Shelf> GetObjectShelves(int objectId, int categoryId)
        {
            return TaskManager.GetObjectShelves(objectId, categoryId);
            
        }
                
        public async Task<List<InStockDto>> GetInStocksAsync(int placeId, int categoryId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            query = query.Where(x => x.CarryoutDate == DateTime.Today && x.Shelf.PlaceId == placeId && x.Shelf.CargoType.CategoryId == categoryId);
            
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }
                
        public async Task<List<InStockDto>> GetInStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.Shelf, x => x.Source, x => x.Worker);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.ShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.Shelf.PlaceId == placeId && x.Shelf.CargoType.CategoryId == categoryId);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }

        public async Task<List<OutStockDto>> GetOutStocksByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId)
        {
            var query = _outStockRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.ShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.Shelf.PlaceId == placeId && x.Shelf.CargoType.CategoryId == categoryId);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<OutStockDto>>(entities);
        }
                
        public async Task<PagedResultDto<InspectDto>> GetInspectsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input)
        {
            var query = _inspectRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            if (carryoutDate.Year > 2000)
                query = query.Where(x => x.CarryoutDate == carryoutDate);
            if (shelfId > 0)
                query = query.Where(x => x.ShelfId == shelfId);
            else
                query = query.Where(x => x.Shelf.PlaceId == placeId && x.Shelf.CargoType.CategoryId == categoryId);


            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = query.OrderByDescending(x => x.CarryoutDate);               // Applying Sorting
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);     // Applying Paging

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return new PagedResultDto<InspectDto>(
                totalCount,
                ObjectMapper.Map<List<InspectDto>>(entities)
            );
        }
                
        public async Task<PagedResultDto<StocktakingDto>> GetStocktakingsByDateAndShelfAsync(DateTime carryoutDate, int shelfId, int placeId, int categoryId, PagedAndSortedResultRequestDto input)
        {
            var query = _stocktakingRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            if (carryoutDate.Year > 2000)
                query = query.Where(x => x.CarryoutDate == carryoutDate);
            if (shelfId > 0)
                query = query.Where(x => x.ShelfId == shelfId);
            else
                query = query.Where(x => x.Shelf.PlaceId == placeId && x.Shelf.CargoType.CategoryId == categoryId);


            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = query.OrderByDescending(x => x.CarryoutDate);               // Applying Sorting
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);     // Applying Paging

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return new PagedResultDto<StocktakingDto>(
                totalCount,
                entities.Select(MapToStocktakingDto).ToList()
            );
        }

        public async Task SubmitStocktaking(int id) 
        {
            var taking = await _stocktakingRepository.GetAsync(id);
            
            if (taking.Deviation.HasValue) return;
            var shelf = await _shelfRepository.GetAsync(taking.ShelfId);

            var deriation = shelf.Inventory.HasValue ? taking.Inventory - shelf.Inventory : taking.Inventory;
            taking.Deviation = deriation;
            shelf.Inventory = taking.Inventory;
        }

        private StocktakingDto MapToStocktakingDto(Stocktaking entity)
        {
            StocktakingDto dto = ObjectMapper.Map<StocktakingDto>(entity);

            if (!dto.Deviation.HasValue)
            {
                var shelf = TaskManager.GetShelf(entity.ShelfId);
                var currentInventory = shelf.Inventory.HasValue ? shelf.Inventory.Value : 0;
                dto.CurrentInventory = string.Format("{0} (偏差为 {1:F2})", currentInventory, dto.Inventory - currentInventory);
            }
            return dto;
        }
    }
}