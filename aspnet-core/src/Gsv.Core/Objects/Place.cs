using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Gsv.Objects
{
    /// <summary>
    /// Category Entity
    /// </summary>
    [Description("监管场地")]
    public class Place : Entity, IMustHaveTenant
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

        /// <summary>
        /// 经度
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Latitude { get; set; }

        [StringLength(GsvConsts.LargeStringFieldLength)]
        public string CameraIps { get; set; }
        
        [ForeignKey("PlaceId")]
        public virtual List<Shelf> Shelves { get; set; }
    }
}

