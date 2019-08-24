using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Gsv.Types;

namespace Gsv.Caches
{
    public class CategoryCache : EntityListCache<Category, Category, Category>, ICategoryCache, ITransientDependency
    {
        public CategoryCache(ICacheManager cacheManager, IRepository<Category> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
}