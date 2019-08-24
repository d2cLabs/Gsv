using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Tasks.Dto
{
    [AutoMap(typeof(Inspect))]
    public class InspectDto : EntityDto
    {
        public DateTime CarryoutTime { get; set; }

        public string WorkerName { get; set; }

        public string PlaceShelfName { get; set; }

        /// <summary>
        /// 成色
        /// </summary>
        public float Purity { get; set; }

        public string Remark { get; set; }

        public int PhotoLength { get; set; }

   }
}

