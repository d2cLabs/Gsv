using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMap(typeof(OutStock))]
    public class OutStockDto : EntityDto
    {
        public DateTime CarryoutTime { get; set; }

        public string WorkerName { get; set; }

        public string PlaceShelfName { get; set; }

        public float Quantity { get; set; }
    }
}

