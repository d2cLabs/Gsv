using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects.Dto
{
    [AutoMapFrom(typeof(Object))]
    public class TaskObjectDto : EntityDto
    { 
        public int CapitalId { get; set; }
        public string CapitalCn { get; set; }
        public string CapitalName { get; set; }

        public int PlaceId { get; set; }

        public string PlaceCn { get; set; }
        public string PlaceName { get; set; }
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUnitName { get; set; }

        public int Quantity{ get ; set; }

        public float YellowQuantity { get; set; }

        public float Inventory { get; set; }
    }
}

