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
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        // 所属场地
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

        public virtual CargoType CargoType { get; set; }

        public float? Inventory { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}

