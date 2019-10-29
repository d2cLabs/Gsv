using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Gsv.Authorization;
using Gsv.Caches;
using Gsv.Objects.Dto;
using Gsv.Tasks;

namespace Gsv.Objects
{
    [AbpAuthorize(PermissionNames.Pages_Objects, PermissionNames.Pages_Watcher, PermissionNames.Pages_Supervisor)]
    public class ObjectAppService : GsvAppServiceBase, IObjectAppService
    {
        public TaskManager TaskManager { get; set; }
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private readonly IPlaceCache _placeCache;
        private readonly ICapitalCache _capitalCache;

        private readonly ICargoTypeCache _cargoTypeCache;
        private readonly IRepository<Object> _objectRepository;

        public ObjectAppService(IPlaceCache placeCache,
            ICapitalCache capitalCache,
            ICargoTypeCache cargoTypeCache,
            IRepository<Object> objectRepository)
        {
            _placeCache = placeCache;
            _capitalCache = capitalCache;
            _cargoTypeCache = cargoTypeCache;
            _objectRepository = objectRepository;
        }

        public List<Place> GetPlaces()
        {
            return _placeCache.GetList();
        }
            
        public List<Capital> GetCapitals()
        {
            return _capitalCache.GetList();
        }

        public List<CargoType> GetCargoTypes(int placeId)
        {
            if (placeId == 0)
                return _cargoTypeCache.GetList();
            return _cargoTypeCache.GetList().FindAll(x => x.PlaceId == placeId);
        }
        
        public async Task<List<TaskObjectDto>> GetObjectsAsync(string sorting)
        {
            var user = await UserManager.GetUserByIdAsync(AbpSession.UserId??0);
            var worker = TaskManager.GetWorkerByCn(user.UserName);
            var query = _objectRepository.GetAllIncluding(x => x.Capital, x => x.Place, x => x.Category);
            if (worker != null && !string.IsNullOrEmpty(worker.PlaceList))
            {
                query = query.Where(x => worker.PlaceList.Contains(x.Place.Cn));
            }

            query = query.OrderBy(sorting);                           // Applying Sorting
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            return entities.Select(MapToTaskObjectDto).ToList();
        }
       
        #region private

        private double GetRatio(Shelf shelf, double quantity)
        {
            var cargoType = TaskManager.GetCargoType(shelf.CargoTypeId);
            return cargoType.Ratio * quantity;
        }

        private TaskObjectDto MapToTaskObjectDto(Object entity)
        {
            var dto = ObjectMapper.Map<TaskObjectDto>(entity);

            var shelfs = TaskManager.GetObjectShelves(entity.Id, entity.CategoryId);

            double sumInventory = 0;
            double sumQuantityInToday = 0;
            double sumQuantityOutToday = 0;
            double sumInventoryInToday = 0;
            double sumInventoryOutToday = 0;
            int sumNumInToday = 0;
            int sumNumOutToday = 0;
            foreach (var shelf in shelfs)
            {
                sumInventory += shelf.Inventory.HasValue ? shelf.Inventory.Value : 0;
                if (shelf.LastInTime.HasValue && shelf.LastInTime.Value.Date == DateTime.Now.Date)
                {
                    sumNumInToday += shelf.NumInToday;
                    sumQuantityInToday += shelf.QuantityInToday;
                    sumInventoryInToday += GetRatio(shelf, shelf.QuantityInToday);

                    if (shelf.LastInTime.Value > dto.LastInTime)
                        dto.LastInTime = shelf.LastInTime.Value;
                }
                if (shelf.LastOutTime.HasValue && shelf.LastOutTime.Value.Date == DateTime.Now.Date)
                {
                    sumNumOutToday += shelf.NumOutToday;
                    sumQuantityOutToday += shelf.QuantityOutToday;
                    sumInventoryOutToday += GetRatio(shelf, shelf.QuantityOutToday);

                    if (shelf.LastOutTime.Value > dto.LastOutTime)
                        dto.LastOutTime = shelf.LastOutTime.Value;
                 }
            }
            dto.Inventory = sumInventory;
            dto.NumInToday = sumNumInToday;
            dto.NumOutToday = sumNumOutToday;
            dto.QuantityInToday = sumQuantityInToday;
            dto.QuantityOutToday = sumQuantityOutToday;
            dto.InventoryInToday = sumInventoryInToday;
            dto.InventoryOutToday = sumInventoryOutToday;

            dto.Spare = sumInventory - dto.YellowQuantity;
            
            return dto;
        }

        #endregion
    }
}