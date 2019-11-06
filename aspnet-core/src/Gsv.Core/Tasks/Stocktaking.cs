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

        public int ObjectId { get; set;}
        public virtual Objects.Object Object { get; set; }

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

        public double Inventory { get; set; }

        public double? Deviation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Remark { get; set; }

        /// <summary>
        /// 照片文件名
        /// </summary>
        [StringLength(GsvConsts.PhotoFilePathLength)]
        public string PhotoFile { get; set; }
    }
}

