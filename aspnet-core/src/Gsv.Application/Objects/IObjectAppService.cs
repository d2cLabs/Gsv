using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Gsv.Objects.Dto;

namespace Gsv.Objects
{
    public interface IObjectAppService : IApplicationService
    {
        List<Place> GetPlaces(); 
        List<Capital> GetCapitals(); 

        Task<List<ShelfObjectDto>> GetObjects(int placeId);
        List<CargoType> GetCargoTypes(int placeId); 
    
        Task<List<TaskObjectDto>> GetObjectsAsync(string sorting);
    }
}
