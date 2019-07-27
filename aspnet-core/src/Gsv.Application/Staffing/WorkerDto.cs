using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gsv.Staffing
{
    [AutoMap(typeof(Worker))]
    public class WorkerDto : EntityDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(Worker.MaxCnLength)]
        public string Cn { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(Worker.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [StringLength(Worker.MaxPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// 微信设备ID
        /// </summary>
        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string WeixinDeviceId { get ; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(GsvConsts.MobileLength)]
        public string Mobile { get; set; }
    }
}