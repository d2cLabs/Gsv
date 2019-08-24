using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Types.Dto
{
    [AutoMap(typeof(Source))]
    public class SourceDto : EntityDto
    {        
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(Source.MaxCnLength)]
        public string Cn { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(Source.MaxNameLength)]
        public string Name { get; set; }
    }
}