using System;
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

        public int YellowQuantity { get; set; }

        public double Inventory { get; set; }

        public double Spare { get; set; }

        public int NumInToday { get; set; }
        public int NumOutToday { get; set; }
        public double QuantityInToday { get; set; }
        public double QuantityOutToday { get; set; }
        public double InventoryInToday { get; set; }
        public double InventoryOutToday { get; set; }
        public DateTime? LastInTime { get; set; }
        public DateTime? LastOutTime { get; set; }

    }
}

