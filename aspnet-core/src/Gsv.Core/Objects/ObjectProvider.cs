using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Gsv.Caches;
using Gsv.Objects.Cache;

namespace Gsv.Objects
{
    /// <summary>
    /// Depot manager.
    /// Implements domain logic for depot.
    /// </summary>
    public class ObjectProvider : ITransientDependency
    {        
        private readonly IPlaceCache _placeCache;
        private readonly ICapitalCache _capitalCache;

        private readonly ICargoTypeCache _cargoTypeCache;

        public ObjectProvider(
            IPlaceCache placeCache,
            ICapitalCache capitalCache,
            ICargoTypeCache cargoTypeCache)
        {
            _placeCache = placeCache;
            _capitalCache = capitalCache;
            _cargoTypeCache = cargoTypeCache;
        }
        
        public List<ComboboxItemDto> GetComboItems(string name)
        {
            var lst = new List<ComboboxItemDto>();
            switch (name) 
            {
                case "Place":
                    foreach (Place t in _placeCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.Name });
                    break;
                case "Capital":
                    foreach (Capital t in _capitalCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.Name });
                    break;
                case "CargoType":
                    foreach (CargoType t in _cargoTypeCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.TypeName });
                    break;
                default:
                    break;
            }
            return lst;
        }

        public string GetPlaceNameById(int id)
        {
            return _placeCache.Get(id).Name;
        }
        
        public string GetCapitalNameById(int id)
        {
            return _capitalCache.GetById(id).Name;
        }
        public string GetCargoTypeNameById(int id)
        {
            return _cargoTypeCache.GetById(id).TypeName;
        }
    }
}