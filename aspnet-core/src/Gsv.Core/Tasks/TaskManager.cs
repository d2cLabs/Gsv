using System.Collections.Generic;
using Abp.Domain.Services;
using Gsv.Caches;
using Gsv.Objects;
using Gsv.Staffing;
using Gsv.Types;

namespace Gsv.Tasks
{
    public class TaskManager : DomainService, IDomainService
    {
        private readonly IWorkerCache _workerCache;
        private readonly IPlaceCache _placeCache;
        private readonly IObjectCache _objectCache;
        private readonly ICategoryCache _categoryCache;
        private readonly IPlaceShelfCache _shelfCache;
        private readonly ICargoTypeCache _cargoTypeCache;

        public TaskManager(IWorkerCache workerCache,
            IPlaceCache placeCache,
            IObjectCache objectCache,
            ICategoryCache categoryCache, 
            IPlaceShelfCache shelfCache,
            ICargoTypeCache cargoTypeCache)
        {
            _workerCache = workerCache;
            _placeCache = placeCache;
            _objectCache = objectCache;
            _categoryCache = categoryCache;
            _shelfCache = shelfCache;
            _cargoTypeCache = cargoTypeCache;
        }

        #region GetEntities

        public Worker GetWorkerByCn(string cn) 
        {
            var worker = _workerCache.GetList().Find(x => x.Cn == cn);
            if (worker != null) return _workerCache[worker.Id];
            return null;
        }

        public Place GetPlaceByCn(string cn) 
        {
            var place = _placeCache.GetList().Find(x => x.Cn == cn);
            if (place != null) return _placeCache[place.Id];
            return null;
        }

        public List<Object> GetObjectsByPlace(int placeId) 
        {
            return _objectCache.GetList().FindAll(x => x.PlaceId == placeId);
        }

        public Category GetCategory(int id)
        {
            return _categoryCache[id];
        }

        public Object GetObject(int id)
        {
            return _objectCache[id];
        }
        #endregion 

        #region Get ListViewModel of Object

        public string GetObjectPlaceInfo(int id)
        {
            var obj = _objectCache[id];
            return _placeCache[obj.PlaceId].Name;
        }

        public string GetObjectCollateral(int id)
        {
            var obj = _objectCache[id];
            return string.Format("类型: {0}   红线: {1}   黄线{2}", _categoryCache[obj.CategoryId].Name, obj.Quantity, obj.YellowQuantity);
        }

        public List<PlaceShelf> GetObjectShelves(int id)
        {
            var obj = _objectCache[id];
            return _shelfCache.GetList().FindAll(x => x.PlaceId == obj.PlaceId);
        }

        public List<PlaceShelf> GetObjectShelves(int id, int categoryId)
        {
            var obj = _objectCache[id];
            return _shelfCache.GetList().FindAll(x => x.PlaceId == obj.PlaceId && _cargoTypeCache[x.CargoTypeId].CategoryId == categoryId);

        }
        #endregion
    }
}