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
        private readonly IShelfCache _shelfCache;
        private readonly ICargoTypeCache _cargoTypeCache;

        private readonly ISourceCache _sourceCache;

        public TaskManager(IWorkerCache workerCache,
            IPlaceCache placeCache,
            IObjectCache objectCache,
            ICategoryCache categoryCache, 
            IShelfCache shelfCache,
            ICargoTypeCache cargoTypeCache,
            ISourceCache sourceCache)
        {
            _workerCache = workerCache;
            _placeCache = placeCache;
            _objectCache = objectCache;
            _categoryCache = categoryCache;
            _shelfCache = shelfCache;
            _cargoTypeCache = cargoTypeCache;
            _sourceCache = sourceCache;
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

        public Shelf GetShelf(int id)
        {
            return _shelfCache[id];
        }

        public CargoType GetCargoType(int id)
        {
            return _cargoTypeCache[id];
        }

        public List<Source> GetSources()
        {
            return _sourceCache.GetList();
        }
        #endregion 

        #region Get ListViewModel of Object(Weixin)

        private double GetObjectTotalInventory(int id)
        {
            var obj = _objectCache[id];
            double total = 0.0d;
            var shelves = _shelfCache.GetList();
            foreach (var shelf in shelves) {
                var type = _cargoTypeCache[shelf.CargoTypeId];
                if (shelf.PlaceId == obj.PlaceId && type.CategoryId == obj.CategoryId)
                    total += shelf.Inventory.HasValue ? shelf.Inventory.Value : 0.0d;
            }
            return total;
        }

        public string GetObjectPlaceInfo(int id)
        {
            var obj = _objectCache[id];
            return _placeCache[obj.PlaceId].Name;
        }

        public (string, double, int) GetObjectCollateral(int id)
        {
            var obj = _objectCache[id];
            return (_categoryCache[obj.CategoryId].Name, GetObjectTotalInventory(id), obj.YellowQuantity);
        }

        public List<Shelf> GetObjectShelves(int id)
        {
            var obj = _objectCache[id];
            return _shelfCache.GetList().FindAll(x => x.PlaceId == obj.PlaceId);
        }

        public List<Shelf> GetObjectShelves(int id, int categoryId)
        {
            var obj = _objectCache[id];
            var shelves = _shelfCache.GetList();
            
            List<Shelf> ret = new List<Shelf>();
            foreach (var shelf in shelves) {
                var type = _cargoTypeCache[shelf.CargoTypeId];
                if (shelf.PlaceId == obj.PlaceId && type.CategoryId == categoryId)
                    ret.Add(shelf);
            }
            return ret;

        }
        #endregion
    }
}