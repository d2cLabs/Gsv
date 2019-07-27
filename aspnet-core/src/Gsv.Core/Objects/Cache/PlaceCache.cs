using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Gsv.Objects.Cache
{
    public class PlaceCache : IPlaceCache, IEventHandler<EntityChangedEventData<Place>>
    {
        private readonly string CacheName = "CachedPlace";
        private readonly IAbpSession _abpSession;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Place> _placeRepository;

        public PlaceCache(
            ICacheManager cacheManager,
            IRepository<Place> placeRepository,
            IAbpSession abpSession)
        {
            _cacheManager = cacheManager;
            _placeRepository = placeRepository;
            _abpSession = abpSession;
        }

        public List<Place> GetList()
        {
            return _cacheManager.GetCache(CacheName)
                .Get(CacheKey, () => _placeRepository.GetAll().ToList());
        }

        public Place GetById(int id)
        {
            return GetList().FirstOrDefault(d => d.Id == id);
        }
               
        public void HandleEvent(EntityChangedEventData<Place> eventData)
        {
            _cacheManager.GetCache(CacheName).Remove(CacheKey);
        }

        private string CacheKey
        {
            get { return "Places@" + (_abpSession.TenantId ?? 0); }
        }
    }
}