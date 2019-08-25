using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Objects;
using Gsv.Staffing;
using Gsv.Types;

namespace Gsv.Tasks
{
    /// <summary>
    /// 入库 Entity
    /// </summary>
    [Description("入库存")]
    public class InStock : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 2;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }

        public DateTime CarryoutDate { get; set; }

        [Required]
        public int WorkerId { get; set; }
        public virtual Worker CreateWorker { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 场地Id
        /// </summary>
        [Required]
        public int ShelfId { get; set; }
        public virtual PlaceShelf PlaceShelf { get; set; }

        public float Quantity { get; set; }

        public int SourceId { get; set; }
        public virtual Source Source { get; set; }
        
    }
}
