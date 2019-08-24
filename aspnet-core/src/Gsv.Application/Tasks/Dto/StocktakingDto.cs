using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMap(typeof(Stocktaking))]
    public class StocktakingDto : EntityDto
    {
        public DateTime CarryoutTime { get; set; }
        public int ShelfId { get; set; }
        public string PlaceShelfName { get; set; }

        public float Inventory { get; set; }

        public float? Deviation { get; set; }
    }
}

