using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.ObjectMapping;
using Abp.Runtime.Caching;

namespace Gsv.Caches
{

    public class EntityListCache<TEntity, TCacheItem, TListItem> : IEventHandler<EntityChangedEventData<TEntity>>, IEventHandler, IEntityListCache<TEntity, TCacheItem, TListItem> 
        where TEntity : class, IEntity where TCacheItem : class
    {
        // private  readonly ITypedCache<int, TEntity> _internalCache;
        private readonly ICacheManager _cacheManager;
        private IRepository<TEntity> _repository;
        private readonly IObjectMapper _objectMapper;
        private readonly string _cacheName;
        public EntityListCache(ICacheManager cacheManager, IRepository<TEntity> repository, IObjectMapper objectMapper, string cacheName = null)
        {
            _cacheManager = cacheManager;
            _repository = repository;
            _objectMapper = objectMapper;
            if (cacheName == null)
            {   
                _cacheName = typeof(TEntity).FullName;
            }
            else
            {
                _cacheName = cacheName;
            }
            ICache cache = _cacheManager.GetCache(_cacheName);
            cache.DefaultSlidingExpireTime = TimeSpan.FromHours(GsvConsts.EntityListCacheSlidingExpireTime);
        }

        #region IEventHandler
        public void HandleEvent(EntityChangedEventData<TEntity> eventData)
        {
            string key = eventData.Entity.Id.ToString();
            _cacheManager.GetCache(_cacheName).Remove(key);
            _cacheManager.GetCache(_cacheName).Remove("List");     // EntityList
        }
        #endregion

        #region IEntityList<TEntity>
        public TCacheItem this[int id] { get => Get(id); }
       
        public TCacheItem Get(int id) 
        {
            var cacheKey = id.ToString();
            return _cacheManager.GetCache(_cacheName)
                .Get(cacheKey, () => _objectMapper.Map<TCacheItem>(_repository.Get(id)));
        }

        public List<TListItem> GetList()
        {
            var cacheKey = "List";
            return _cacheManager.GetCache(_cacheName)
                .Get(cacheKey, () =>  _objectMapper.Map<List<TListItem>>(_repository.GetAllList()));
        }

        #endregion 

    }
}