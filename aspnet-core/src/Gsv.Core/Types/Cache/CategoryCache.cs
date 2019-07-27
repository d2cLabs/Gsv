using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Gsv.Types.Cache
{
    public class CategoryCache : ICategoryCache, IEventHandler<EntityChangedEventData<Category>>
    {
        private readonly string CacheName = "CachedCategory";
        private readonly IAbpSession _abpSession;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryCache(
            ICacheManager cacheManager,
            IRepository<Category> categoryRepository,
            IAbpSession abpSession)
        {
            _cacheManager = cacheManager;
            _categoryRepository = categoryRepository;
            _abpSession = abpSession;
        }

        public List<Category> GetList()
        {
            return _cacheManager.GetCache(CacheName)
                .Get(CacheKey, () => _categoryRepository.GetAll().ToList());
        }

        public Category GetById(int id)
        {
            return GetList().FirstOrDefault(d => d.Id == id);
        }
        
        public void HandleEvent(EntityChangedEventData<Category> eventData)
        {
            _cacheManager.GetCache(CacheName).Remove(CacheKey);
        }

        private string CacheKey
        {
            get { return "Categories@" + (_abpSession.TenantId ?? 0); }
        }
    }
}