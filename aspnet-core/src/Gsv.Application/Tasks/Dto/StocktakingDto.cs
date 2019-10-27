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

        public double Inventory { get; set; }

        public double ActualInventory { get; set; }
        public string CurrentInventory { get; set; }
        
        public double? Deviation { get; set; }

        public string WorkerName { get; set; }
        public string Remark { get; set; }

        public string PhotoFile { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

