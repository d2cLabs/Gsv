using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Gsv.Objects.Cache
{
    public class CapitalCache : ICapitalCache, IEventHandler<EntityChangedEventData<Capital>>
    {
        private readonly string CacheName = "CachedCapital";
        private readonly IAbpSession _abpSession;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Capital> _capitalRepository;

        public CapitalCache(
            ICacheManager cacheManager,
            IRepository<Capital> capitalRepository,
            IAbpSession abpSession)
        {
            _cacheManager = cacheManager;
            _capitalRepository = capitalRepository;
            _abpSession = abpSession;
        }

        public List<Capital> GetList()
        {
            return _cacheManager.GetCache(CacheName)
                .Get(CacheKey, () => _capitalRepository.GetAll().ToList());
        }

        public Capital GetById(int id)
        {
            return GetList().FirstOrDefault(d => d.Id == id);
        }
        
        public void HandleEvent(EntityChangedEventData<Capital> eventData)
        {
            _cacheManager.GetCache(CacheName).Remove(CacheKey);
        }

        private string CacheKey
        {
            get { return "Capitals@" + (_abpSession.TenantId ?? 0); }
        }
    }
}