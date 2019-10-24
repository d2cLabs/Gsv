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

            // query = query.OrderBy(sorting);                           // Applying Sorting

            try {
            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            return entities.Select(MapToTaskObjectDto).ToList();
            }
            catch (System.Exception ex)
            {
            }       //return entities.Select(MapToTaskObjectDto).ToList();
            return null;
        }
       
        #region private
        private TaskObjectDto MapToTaskObjectDto(Object entity)
        {
            var dto = ObjectMapper.Map<TaskObjectDto>(entity);

            var shelfs = TaskManager.GetObjectShelves(entity.Id, entity.CategoryId);

            double sum = 0;
            foreach (var shelf in shelfs)
            {
                sum += shelf.Inventory.HasValue ? shelf.Inventory.Value : 0;
            }
            dto.Inventory = sum;
            return dto;
        }

        #endregion
    }
}