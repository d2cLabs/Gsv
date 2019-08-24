using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gsv.Staffing
{
    /// <summary>
    /// Worker Entity
    /// </summary>
    [Description("工作人员")]
    public class Worker : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 6;
        public const int MaxNameLength = 12;
        public const int MaxPasswordLength = 12;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(MaxCnLength)]
        public string Cn { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public string Password { get; set; }

        [StringLength(GsvConsts.MobileLength)]
         public string Mobile { get; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string PlaceList { get ; set; }
    }
}

