using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Gsv.Objects;

namespace Gsv.Caches
{
    public class PlaceCache : EntityListCache<Place, Place, Place>, IPlaceCache, ITransientDependency
    {
        public PlaceCache(ICacheManager cacheManager, IRepository<Place> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
    
    public class ShelfCache : EntityListCache<Shelf, Shelf, Shelf>, IShelfCache, ITransientDependency
    {
        public ShelfCache(ICacheManager cacheManager, IRepository<Shelf> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
    
    public class CargoTypeCache : EntityListCache<CargoType, CargoType, CargoType>, ICargoTypeCache, ITransientDependency
    {
        public CargoTypeCache(ICacheManager cacheManager, IRepository<CargoType> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
    
    public class CapitalCache : EntityListCache<Capital, Capital, Capital>, ICapitalCache, ITransientDependency
    {
        public CapitalCache(ICacheManager cacheManager, IRepository<Capital> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
    
    public class ObjectCache : EntityListCache<Object, Object, Object>, IObjectCache, ITransientDependency
    {
        public ObjectCache(ICacheManager cacheManager, IRepository<Object> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
}