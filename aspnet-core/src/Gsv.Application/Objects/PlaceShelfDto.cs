using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects
{
    [AutoMap(typeof(PlaceShelf))]
    public class PlaceShelfDto : EntityDto
    {
        /// <summary>
        /// 场地货架名称
        /// </summary>
        [Required]
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Name { get; set; }

        /// <summary>
        /// 编号（识别号）
        /// </summary>
        [StringLength(PlaceShelf.MaxIdentifierLength)]
        public string Identifier { get; set; }
        
        /// <summary>
        /// PlaceCargoTypeList
        /// </summary>
        [Required]
        public int CargoTypeId { get; set; }

        //public float CurrentInventory { get; set; }
        //public DateTime InventoryLastTime { get; set; }
    }
}