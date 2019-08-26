using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Objects;
using Gsv.Staffing;

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


        public DateTime CarryoutDate { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 场地Id
        /// </summary>
        [Required]
        public int ShelfId { get; set; }
        public virtual Shelf Shelf { get; set; }

        public float Inventory { get; set; }

        public float? Deviation { get; set; }
    }
}

