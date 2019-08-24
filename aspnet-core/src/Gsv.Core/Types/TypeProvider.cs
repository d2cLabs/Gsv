using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Gsv.Caches;

namespace Gsv.Types
{
    /// <summary>
    /// Depot manager.
    /// Implements Typs Manager.
    /// </summary>
    public class TypeProvider : ITransientDependency
    {        
        private readonly ICategoryCache _categoryCache;


        public TypeProvider(
            ICategoryCache categoryCache)
        {
            _categoryCache = categoryCache;
        }
        
        public List<ComboboxItemDto> GetComboItems(string tableName)
        {
            var lst = new List<ComboboxItemDto>();
            switch (tableName) 
            {
                case "Category":
                    foreach (Category t in _categoryCache.GetList())
                        lst.Add(new ComboboxItemDto { Value = t.Id.ToString(), DisplayText = t.Name });
                    break;
                default:
                    break;
            }
            return lst;
        }
    }
}
 