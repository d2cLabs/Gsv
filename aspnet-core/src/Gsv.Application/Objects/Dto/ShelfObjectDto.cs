using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects.Dto
{
    [AutoMapFrom(typeof(Object))]
    public class ShelfObjectDto : EntityDto
    { 
        public int CapitalId { get; set; }
        public string CapitalCn { get; set; }
        public string CapitalName { get; set; }

        public int PlaceId { get; set; }

        public string PlaceCn { get; set; }
        public string PlaceName { get; set; }
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

