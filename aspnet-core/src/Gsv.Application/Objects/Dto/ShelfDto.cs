using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects.Dto
{
    [AutoMap(typeof(Shelf))]
    public class ShelfDto : EntityDto
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

        // public float Inventory { get; set; }
    }
}