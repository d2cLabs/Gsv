using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMap(typeof(Allot))]
    public class AllotDto : EntityDto
    {
        public DateTime CarryoutDate { get; set; }

        public string WorkerName { get; set; }

        public string FromShelfName { get; set; }
        public string ToShelfName { get; set; }

        /// <summary>
        /// 成色
        /// </summary>
        public double Quantity { get; set; }

        public string Remark { get; set; }

        public int PhotoLength { get; set; }

        public DateTime CreateTime { get; set; }

   }
}

