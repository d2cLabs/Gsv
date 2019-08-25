using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;

namespace Gsv.Objects
{
    public interface IObjectAppService : IApplicationService
    {
        List<ComboboxItemDto> GetComboItems(string name); 

        Task<List<TaskObjectDto>> GetObjectsAsync(string sorting);
    }
}
