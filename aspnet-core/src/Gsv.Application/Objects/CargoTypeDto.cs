using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects
{
    [AutoMap(typeof(CargoType))]
    public class CargoTypeDto : EntityDto
    {
        /// <summary>
        /// 场地Id
        /// </summary>
        public int PlaceId { get; set; }
        
        /// <summary>
        /// 品类Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(CargoType.MaxNameLength)]
        public string TypeName { get ; set; }

        [Required]
        public float Ratio { get; set; }
    }
}