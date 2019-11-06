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

        private const int NumItems = 20;
        private readonly IRepository<Allot> _allotRepository;
        private readonly IRepository<InStock> _inStockRepository;
        private readonly IRepository<OutStock> _outStockRepository;
        private readonly IRepository<Inspect> _inspectRepository;
        private readonly IRepository<Stocktaking> _stocktakingRepository;

        private readonly IRepository<Shelf> _shelfRepository;

        public TaskAppService(IRepository<Allot> allotRepository,
            IRepository<InStock> inStockRepository,
            IRepository<OutStock> outStockRepository,
            IRepository<Inspect> inspectRepository,
            IRepository<Stocktaking> stocktakingRepository, 
            IRepository<Shelf> shelfRepository)
        {
            _allotRepository = allotRepository;
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
        
        public List<TaskShelfDto> GetObjectShelves(int objectId)
        {
            var shelves = TaskManager.GetObjectShelves(objectId);
            return ObjectMapper.Map<List<TaskShelfDto>>(shelves);           
        }

        public async Task<List<AllotDto>> GetAllotsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId)
        {
            var query = _allotRepository.GetAllIncluding(x => x.FromShelf, x => x.ToShelf, x => x.Worker).Where(x => x.ObjectId == objectId);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.FromShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<AllotDto>>(entities);
        }
                
        public async Task<List<InStockDto>> GetInStocksByObjectAsync(int objectId, DateTime carryoutDate, int shelfId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.Shelf, x => x.Source, x => x.Worker).Where(x => x.ObjectId == objectId);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.ShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }

        public async Task<List<OutStockDto>> GetOutStocksByObjectAsync(int objectId, DateTime carryoutDate, int shelfId)
        {
            var query = _outStockRepository.GetAllIncluding(x => x.Shelf, x => x.Worker).Where(x => x.ObjectId == objectId);
            if (shelfId > 0)
                query = query.Where(x => x.CarryoutDate == carryoutDate && x.ShelfId == shelfId);
            else
                query = query.Where(x => x.CarryoutDate == carryoutDate);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<OutStockDto>>(entities);
        }
                
        public async Task<PagedResultDto<InspectDto>> GetInspectsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId, PagedAndSortedResultRequestDto input)
        {
            var query = _inspectRepository.GetAllIncluding(x => x.Shelf, x => x.Worker).Where(x => x.ObjectId == objectId);
            if (carryoutDate.Year > 2000) 
                query = query.Where(x => x.CarryoutDate == carryoutDate);
            if (shelfId > 0)
                query = query.Where(x => x.ShelfId == shelfId);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = query.OrderByDescending(x => x.CarryoutDate);               // Applying Sorting
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);     // Applying Paging

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return new PagedResultDto<InspectDto>(
                totalCount,
                ObjectMapper.Map<List<InspectDto>>(entities)
            );
        }
                
        public async Task<PagedResultDto<StocktakingDto>> GetStocktakingsByObjectAsync(int objectId, DateTime carryoutDate, int shelfId, PagedAndSortedResultRequestDto input)
        {
            var query = _stocktakingRepository.GetAllIncluding(x => x.Shelf, x => x.Worker).Where(x => x.ObjectId == objectId);
            if (carryoutDate.Year > 2000)
                query = query.Where(x => x.CarryoutDate == carryoutDate);
            if (shelfId > 0)
                query = query.Where(x => x.ShelfId == shelfId);

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

            var actual = GetRatio(shelf, taking.Inventory);
            var deriation = shelf.Inventory.HasValue ? actual - shelf.Inventory : actual;
            taking.Deviation = deriation;
            shelf.Inventory = actual;
        }

        public async Task DeleteStocktaking(int id) 
        {
            var taking = await _stocktakingRepository.GetAsync(id);
            if (taking.Deviation.HasValue) return;
            _stocktakingRepository.Delete(taking);           
        }


        public async Task DeleteInStock(int id) 
        {
            var inStock = await _inStockRepository.GetAsync(id);
            
            var shelf = await _shelfRepository.GetAsync(inStock.ShelfId);
            shelf.Inventory -= GetRatio(shelf, inStock.Quantity);
            _inStockRepository.Delete(inStock);
        }

        public async Task DeleteOutStock(int id) 
        {
            var outStock = await _outStockRepository.GetAsync(id);
            
            var shelf = await _shelfRepository.GetAsync(outStock.ShelfId);
            shelf.Inventory += GetRatio(shelf, outStock.Quantity);
            _outStockRepository.Delete(outStock);
        }

        public async Task DeleteAllot(int id) 
        {
            var allot = await _allotRepository.GetAsync(id);
            
            var shelf = await _shelfRepository.GetAsync(allot.FromShelfId);
            shelf.Inventory += GetRatio(shelf, allot.Quantity);
            shelf = await _shelfRepository.GetAsync(allot.ToShelfId);
            shelf.Inventory -= GetRatio(shelf, allot.Quantity);
            _allotRepository.Delete(allot);
        }

        private StocktakingDto MapToStocktakingDto(Stocktaking entity)
        {
            StocktakingDto dto = ObjectMapper.Map<StocktakingDto>(entity);
            var shelf = TaskManager.GetShelf(entity.ShelfId);
            var cargoType = TaskManager.GetCargoType(shelf.CargoTypeId);
            dto.ActualInventory = cargoType.Ratio * dto.Inventory;

            if (!dto.Deviation.HasValue)
            {
                var currentInventory = shelf.Inventory.HasValue ? shelf.Inventory.Value : 0;
                dto.CurrentInventory = string.Format("{0} (偏差为 {1:F3})", currentInventory, dto.ActualInventory - currentInventory);
            }
            return dto;
        }

        private double GetRatio(Shelf shelf, double quantity)
        {
            var cargoType = TaskManager.GetCargoType(shelf.CargoTypeId);
            double r = quantity * cargoType.Ratio;
            return  r;
        }
        #region Wx

        [AbpAllowAnonymous]
        public async Task<List<AllotDto>> GetWxAllotsAsync(int objectId)
        {
            var query = _allotRepository.GetAllIncluding(x => x.FromShelf, x => x.Worker);
            query = query.Where(x => x.ObjectId == objectId && x.CarryoutDate == DateTime.Today);
            query = query.OrderByDescending(x => x.CreateTime).Take(NumItems);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<AllotDto>>(entities);
        }
        [AbpAllowAnonymous]
        public async Task<List<InStockDto>> GetWxInStocksAsync(int objectId)
        {
            var query = _inStockRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            query = query.Where(x => x.ObjectId == objectId && x.CarryoutDate == DateTime.Today);
            query = query.OrderByDescending(x => x.CreateTime).Take(NumItems);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InStockDto>>(entities);
        }
        [AbpAllowAnonymous]
        public async Task<List<OutStockDto>> GetWxOutStocksAsync(int objectId)
        {
            var query = _outStockRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            query = query.Where(x => x.ObjectId == objectId && x.CarryoutDate == DateTime.Today);
            query = query.OrderByDescending(x => x.CreateTime).Take(NumItems);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<OutStockDto>>(entities);
        }
                
        [AbpAllowAnonymous]
        public async Task<List<InspectDto>> GetWxInspectsAsync(int objectId)
        {
            var query = _inspectRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            query = query.Where(x => x.ObjectId == objectId && x.CarryoutDate == DateTime.Today);
            query = query.OrderByDescending(x => x.CreateTime).Take(NumItems);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<InspectDto>>(entities);
        }
                
        [AbpAllowAnonymous]
        public async Task<List<StocktakingDto>> GetWxStocktakingsAsync(int objectId)
        {
            var query = _stocktakingRepository.GetAllIncluding(x => x.Shelf, x => x.Worker);
            query = query.Where(x => x.ObjectId == objectId && x.CarryoutDate == DateTime.Today);
            query = query.OrderByDescending(x => x.CreateTime).Take(NumItems);
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            
            return ObjectMapper.Map<List<StocktakingDto>>(entities);
        }

        [AbpAllowAnonymous]
        public void InsertAllot(int objectId, int fromShelfId, int toShelfId, double quantity, string remark, int workerId)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var entity = new Allot() {
                    ObjectId = objectId,
                    CarryoutDate = DateTime.Today,
                    FromShelfId = fromShelfId,
                    ToShelfId = toShelfId,
                    Quantity = quantity,
                    Remark = remark,
                    WorkerId = workerId,
                    CreateTime = DateTime.Now
                };

                _allotRepository.Insert(entity);
           
                var shelf = _shelfRepository.Get(fromShelfId);
                shelf.Inventory -= GetRatio(shelf, quantity);
                // shelf.LastOutTime = DateTime.Now;

                shelf = _shelfRepository.Get(toShelfId);
                shelf.Inventory += GetRatio(shelf, quantity);
                // shelf.LastInTime = DateTime.Now;

                CurrentUnitOfWork.SaveChanges();
            }
        }

                
        [AbpAllowAnonymous]
        public void InsertInStock(int objectId, int shelfId, int sourceId, double quantity, string remark, int workerId)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var entity = new InStock() {
                    ObjectId = objectId,
                    CarryoutDate = DateTime.Today,
                    ShelfId = shelfId,
                    SourceId = sourceId,
                    Quantity = quantity,
                    Remark = remark,
                    WorkerId = workerId,
                    CreateTime = DateTime.Now
                };

                _inStockRepository.Insert(entity);
            
                var shelf = _shelfRepository.Get(shelfId);
                if (!shelf.Inventory.HasValue) shelf.Inventory = 0;
                shelf.Inventory += GetRatio(shelf, quantity);
                if (!shelf.LastInTime.HasValue || shelf.LastInTime.Value.Date != DateTime.Now.Date)
                {
                    shelf.NumInToday = 0;
                    shelf.QuantityInToday = 0;
                }
                shelf.NumInToday += 1;
                shelf.QuantityInToday += quantity;
                shelf.LastInTime = DateTime.Now;

                CurrentUnitOfWork.SaveChanges();
            }
        }

        [AbpAllowAnonymous]
        public void InsertOutStock(int objectId, int shelfId, double quantity, string remark, int workerId)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var entity = new OutStock() {
                    ObjectId = objectId,
                    CarryoutDate = DateTime.Today,
                    ShelfId = shelfId,
                    Quantity = quantity,
                    Remark = remark,
                    WorkerId = workerId,
                    CreateTime = DateTime.Now
                };
                _outStockRepository.Insert(entity);

                var shelf = _shelfRepository.Get(shelfId);
                if (!shelf.Inventory.HasValue) shelf.Inventory = 0;
                shelf.Inventory -= GetRatio(shelf, quantity);
                //shelf.Inventory += GetRatio(shelf, quantity);
                if (!shelf.LastOutTime.HasValue || shelf.LastOutTime.Value.Date != DateTime.Now.Date)
                {
                    shelf.NumOutToday = 0;
                    shelf.QuantityOutToday = 0;
                }
                shelf.NumOutToday += 1;
                shelf.QuantityOutToday += quantity;
                shelf.LastOutTime = DateTime.Now;

                CurrentUnitOfWork.SaveChanges();
            }
        }

        [AbpAllowAnonymous]
        public void InsertInspect(int objectId, int shelfId, float purity, string remark, int workerId)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var entity = new Inspect() {
                    ObjectId = objectId,
                    CarryoutDate = DateTime.Today,
                    ShelfId = shelfId,
                    Purity = purity,
                    Remark = remark,
                    WorkerId = workerId,
                    CreateTime = DateTime.Now
                };
                _inspectRepository.Insert(entity);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        [AbpAllowAnonymous]
        public void InsertStocktaking(int objectId, int shelfId, double inventory, string remark, int workerId)
        {
            using (CurrentUnitOfWork.SetTenantId(1))
            {
                var entity = new Stocktaking() {
                    ObjectId = objectId,
                    CarryoutDate = DateTime.Today,
                    ShelfId = shelfId,
                    Inventory = inventory,
                    Remark = remark,
                    WorkerId = workerId,
                    CreateTime = DateTime.Now
                };
                _stocktakingRepository.Insert(entity);
                CurrentUnitOfWork.SaveChanges();
            }
        }
        #endregion
    }
}