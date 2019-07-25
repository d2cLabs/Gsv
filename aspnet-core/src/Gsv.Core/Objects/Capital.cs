using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gsv.Objects
{
    /// <summary>
    /// Capital Entity
    /// </summary>
    [Description("资金提供方")]
    public class Capital : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 6;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(MaxCnLength)]
        public string Cn { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Name { get; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Contact { get ; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string WenxinIds { get; set; }
    }
}

