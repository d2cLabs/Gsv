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
    public class Shelf : Entity, IMustHaveTenant
    {
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        // 所属场地
        public int PlaceId { get; set; }

        // 所属标的
        public int ObjectId { get; set; }

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

        public double? Inventory { get; set; }
        public int NumInToday { get; set; }
        public int NumOutToday { get; set; }
        public double QuantityInToday { get; set; }
        public double QuantityOutToday { get; set; }
        public DateTime? LastInTime { get; set; }
        public DateTime? LastOutTime { get; set; }
    }
}

