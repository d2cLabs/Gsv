using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Types
{
    [AutoMap(typeof(Category))]
    public class CategoryDto : EntityDto
    {        
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(Category.MaxCnLength)]
        public string Cn { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(Category.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        [Required]
        [StringLength(Category.MaxNameLength)]
        public string UnitName { get; set; }

        public float CurrentPrice { get; set; }
    }
}