using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;
using Gsv.Staffing;

namespace Gsv.Caches
{
    public class WorkerCache : EntityListCache<Worker, Worker, Worker>, IWorkerCache, ITransientDependency
    {
        public WorkerCache(ICacheManager cacheManager, IRepository<Worker> repository, IObjectMapper objectMapper)
            : base(cacheManager, repository, objectMapper)
        {
        }
    }
}