using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gsv.Types
{
    /// <summary>
    /// Category Entity
    /// </summary>
    [Description("进货来源")]
    public class Source : Entity, IMustHaveTenant
    {
        public const int MaxCnLength = 2;
        public const int MaxNameLength = 8;
 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(MaxCnLength)]
        public string Cn { get; set; }
        
        /// <summary>
        /// 品类名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }
    }
}

