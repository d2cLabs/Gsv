using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects
{
    [AutoMap(typeof(Object))]
    public class ObjectDto : EntityDto
    { 
        /// <summary>
        /// 资本Id
        /// </summary>
        [Required]
        public int CapitalId { get; set; }

        /// <summary>
        /// 场地Id
        /// </summary>
        [Required]
        public int PlaceId { get; set; }
        
        /// <summary>
        /// 品类Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public float Quantity{ get ; set; }
        public float YellowQuantity { get; set; }

        public string isFixedPrice { get; set; }

        public float? FixedPrice { get; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string Remark { get; set; }
    }
}
