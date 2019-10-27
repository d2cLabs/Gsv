using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Gsv.Objects;

namespace Gsv.Tasks.Dto
{
    [AutoMapFrom(typeof(Shelf))]
    public class TaskShelfDto : EntityDto
    {
        [Required]
        public int PlaceId { get; set; }
        
        /// <summary>
        /// 场地货架名称
        /// </summary>
        [Required]
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Name { get; set; }

        
        /// <summary>
        /// CargoType
        /// </summary>
        [Required]
        public int CargoTypeId { get; set; }

        public double? Inventory { get; set; }
    }
}