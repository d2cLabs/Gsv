using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMap(typeof(Stocktaking))]
    public class StocktakingDto : EntityDto
    {
        public DateTime CarryoutDate { get; set; }
        public int ShelfId { get; set; }
        public string ShelfName { get; set; }

        public float Inventory { get; set; }

        public float ActualInventory { get; set; }
        public string CurrentInventory { get; set; }
        
        public float? Deviation { get; set; }

        public string WorkerName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

