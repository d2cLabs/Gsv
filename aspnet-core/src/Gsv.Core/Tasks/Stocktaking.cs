using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Objects;

namespace Gsv.Tasks
{
    /// <summary>
    /// 盘库 Entity
    /// </summary>
    [Description("场地货架库存")]
    public class Stocktaking : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 2;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }


        public DateTime CarryoutTime { get; set; }
        /// <summary>
        /// 场地Id
        /// </summary>
        [Required]
        public int ShelfId { get; set; }
        public virtual PlaceShelf PlaceShelf { get; set; }

        public float Inventory { get; set; }

        public float Deviation { get; set; }
    }
}

