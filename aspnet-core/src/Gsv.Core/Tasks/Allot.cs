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
    /// 调拨 Entity
    /// </summary>
    [Description("调拨")]
    public class Allot : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 2;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }


        public int ObjectId { get; set;}
        public virtual Objects.Object Object { get; set; }


        public DateTime CarryoutDate { get; set; }

        [Required]
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 调出场地Id
        /// </summary>
        [Required]
        public int FromShelfId { get; set; }
        public virtual Shelf FromShelf { get; set; }

        /// <summary>
        /// 调入场地Id
        /// </summary>
        [Required]
        public int ToShelfId { get; set; }
        public virtual Shelf ToShelf { get; set; }

        public double Quantity { get; set; }
        
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

