using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Types;

namespace Gsv.Objects
{
    /// <summary>
    /// CargoType Entity
    /// </summary>
    [Description("货物类型")]
    public class CargoType : Entity, IMustHaveTenant
    { 
        public const int MaxNameLength = 12;

        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 场地Id
        /// </summary>
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        
        /// <summary>
        /// 品类Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string TypeName { get ; set; }

        [Required]
        public float Ratio { get; set; }
    }
}

