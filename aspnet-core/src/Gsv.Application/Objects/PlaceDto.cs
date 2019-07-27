using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects
{
    [AutoMap(typeof(Place))]
    public class PlaceDto : EntityDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(Place.MaxCnLength)]
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
    }
}