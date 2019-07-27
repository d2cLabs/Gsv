using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;

namespace Gsv.Objects
{
    [AbpAuthorize]
    public class ObjectAppService : GsvAppServiceBase, IObjectAppService
    {
        private readonly ObjectProvider _objectProvider;

        public ObjectAppService(ObjectProvider objectProvider)
        {
            _objectProvider = objectProvider;
        }

        public Task<List<ComboboxItemDto>> GetComboItems(string name)
        {
            List<ComboboxItemDto> lst = _objectProvider.GetComboItems(name);
            return Task.FromResult<List<ComboboxItemDto>>(lst);
        }        
    }
}