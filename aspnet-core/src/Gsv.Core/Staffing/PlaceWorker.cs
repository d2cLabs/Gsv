using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Gsv.Objects;

namespace Gsv.Staffing
{
    /// <summary>
    /// PlaceWorker Entity
    /// </summary>
    [Description("场地工作人员")]
    public class PlaceWorker : Entity, IMustHaveTenant
    { 
        // Implement of IMustHaveTenant
        public int TenantId { get; set; }
       
        /// <summary>
        /// 场地Id
        /// </summary>
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        [StringLength(GsvConsts.NormalStringFieldLength)]
        public string WorkerList { get ; set; }
    }
}

