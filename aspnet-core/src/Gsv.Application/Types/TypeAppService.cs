using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;

namespace Gsv.Types
{
    [AbpAuthorize]
    public class TypeAppService : GsvAppServiceBase, ITypeAppService
    {
        private readonly TypeProvider _typeProvider;

        public TypeAppService(TypeProvider typeProvider)
        {
            _typeProvider = typeProvider;
        }

        public Task<List<ComboboxItemDto>> GetComboItems(string typeName)
        {
            List<ComboboxItemDto> lst = _typeProvider.GetComboItems(typeName);
            return Task.FromResult<List<ComboboxItemDto>>(lst);
        }
        
        
    }

}