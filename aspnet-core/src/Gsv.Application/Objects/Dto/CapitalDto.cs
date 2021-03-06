using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Objects.Dto
{
    [AutoMap(typeof(Capital))]
    public class CapitalDto : EntityDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(Capital.MaxCnLength)]
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