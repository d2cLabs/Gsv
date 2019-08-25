using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Gsv.Authorization;
using Gsv.Caches;

namespace Gsv.Objects
{
    [AbpAuthorize(PermissionNames.Pages_Objects, PermissionNames.Pages_Watcher)]
    public class ObjectAppService : GsvAppServiceBase, IObjectAppService
    {
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

        public List<ComboboxItemDto> GetComboItems(string name)
        {
            var lst = new List<ComboboxItemDto>();
            switch (name) 
            {
                case "Place":
                    foreach (Place t in _placeCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.Name });
                    break;
                case "Capital":
                    foreach (Capital t in _capitalCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.Name });
                    break;
                case "CargoType":
                    foreach (CargoType t in _cargoTypeCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.TypeName });
                    break;
                default:
                    break;
            }
            return lst;
        }


        public async Task<List<TaskObjectDto>> GetObjectsAsync(string sorting)
        {
            var query = _objectRepository.GetAllIncluding(x => x.Capital, x => x.Place, x => x.Category);

            query = query.OrderBy(sorting);                           // Applying Sorting

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return entities.Select(MapToTaskObjectDto).ToList();
        }
       
        #region private
        private TaskObjectDto MapToTaskObjectDto(Object entity)
        {
            var dto = ObjectMapper.Map<TaskObjectDto>(entity);

            return dto;
        }

        #endregion
    }
}