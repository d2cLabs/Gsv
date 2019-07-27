using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Gsv.Objects.Cache
{
    public class CargoTypeCache : ICargoTypeCache, IEventHandler<EntityChangedEventData<CargoType>>
    {
        private readonly string CacheName = "CachedCargoType";
        private readonly IAbpSession _abpSession;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<CargoType> _cargoTypeRepository;

        public CargoTypeCache(
            ICacheManager cacheManager,
            IRepository<CargoType> cargoTypeRepository,
            IAbpSession abpSession)
        {
            _cacheManager = cacheManager;
            _cargoTypeRepository = cargoTypeRepository;
            _abpSession = abpSession;
        }

        public List<CargoType> GetList()
        {
            return _cacheManager.GetCache(CacheName)
                .Get(CacheKey, () => _cargoTypeRepository.GetAll().ToList());
        }

        public CargoType GetById(int id)
        {
            return GetList().FirstOrDefault(d => d.Id == id);
        }
        
        public void HandleEvent(EntityChangedEventData<CargoType> eventData)
        {
            _cacheManager.GetCache(CacheName).Remove(CacheKey);
        }

        private string CacheKey
        {
            get { return "CargoTypes@" + (_abpSession.TenantId ?? 0); }
        }
    }
}