using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMapFrom(typeof(InStock))]
    public class InStockDto : EntityDto
    {
        public DateTime CarryoutDate { get; set; }

        public string WorkerName { get; set; }

        public string ShelfName { get; set; }

        public double Quantity { get; set; }

        public int SourceId { get; set; }
        public string SourceName { get; set; }

        public DateTime CreateTime { get; set; }
        
    }
}

