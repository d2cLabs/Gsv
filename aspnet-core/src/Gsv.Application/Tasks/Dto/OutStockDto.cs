using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMapFrom(typeof(OutStock))]
    public class OutStockDto : EntityDto
    {
        public DateTime CarryoutDate { get; set; }

        public string WorkerName { get; set; }

        public string ShelfName { get; set; }

        public double Quantity { get; set; }

        public string Remark { get; set; }

        public string PhotoFile { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

