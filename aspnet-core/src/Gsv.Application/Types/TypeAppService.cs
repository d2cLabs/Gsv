using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Gsv.Caches;

namespace Gsv.Types
{
    [AbpAuthorize]
    public class TypeAppService : GsvAppServiceBase, ITypeAppService
    {
        private readonly ICategoryCache _categoryCache;

        public TypeAppService(ICategoryCache categoryCache)
        {
            _categoryCache = categoryCache;
        }

        public List<Category> GetCategories()
        {
            return _categoryCache.GetList();
        }       
    }

}