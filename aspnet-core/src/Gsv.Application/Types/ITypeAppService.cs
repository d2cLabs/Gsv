using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;

namespace Gsv.Types
{
    public interface ITypeAppService : IApplicationService
    {
        Task<List<ComboboxItemDto>> GetComboItems(string typeName);
    }
}
