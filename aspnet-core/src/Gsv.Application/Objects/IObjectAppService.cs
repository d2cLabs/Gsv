using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;

namespace Gsv.Objects
{
    public interface IObjectAppService : IApplicationService
    {
        Task<List<ComboboxItemDto>> GetComboItems(string name); 
    }
}
