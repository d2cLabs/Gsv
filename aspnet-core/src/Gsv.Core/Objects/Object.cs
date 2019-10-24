using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Types;

namespace Gsv.Objects
{
    /// <summary>
    /// Object Entity
    /// </summary>
    [Description("标的")]
    public class Object : Entity, IMustHaveTenant
    { 
        public const int MaxNameLength = 12;

        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 资本Id
        /// </summary>
        [Required]
        public int CapitalId { get; set; }
        public virtual Capital Capital { get; set; }

        /// <summary>
        /// 场地Id
        /// </summary>
        [Required]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        
        /// <summary>
        /// 品类Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int Quantity{ get ; set; }
        public int YellowQuantity{ get ; set; }

        public bool isFixedPrice { get; set; }

        public float? FixedPrice { get; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Remark { get; set; }
    }
}

