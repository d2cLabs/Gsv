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
    
    public class PlaceShelfCache : EntityListCache<PlaceShelf, PlaceShelf, PlaceShelf>, IPlaceShelfCache, ITransientDependency
    {
        public PlaceShelfCache(ICacheManager cacheManager, IRepository<PlaceShelf> repository, IObjectMapper objectMapper)
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