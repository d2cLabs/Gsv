using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gsv.Objects
{
    /// <summary>
    /// Category Entity
    /// </summary>
    [Description("场地货架")]
    public class PlaceShelf : Entity, IMustHaveTenant
    {
        public const int MaxIdentifierLength = 12;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 场地货架名称
        /// </summary>
        [Required]
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Name { get; set; }

        /// <summary>
        /// 编号（识别号）
        /// </summary>
        [Required]
        [StringLength(MaxIdentifierLength)]
        public string Identifier { get; set; }
        
        /// <summary>
        /// PlaceCargoTypeList
        /// </summary>
        [Required]
        public int CargoTypeId { get; set; }

        public virtual CargoType CargoType { get; set; }

        public float CurrentInventory { get; set; }
        public DateTime InventoryLastTime { get; set; }
    }
}

