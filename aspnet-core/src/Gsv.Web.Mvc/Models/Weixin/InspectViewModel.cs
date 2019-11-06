using System.Collections.Generic;
using Gsv.Objects.Dto;

namespace Gsv.Web.Models.Weixin
{
    public class InspectViewModel
    {
        public int ObjectId { get; set; }
        public int ShelfId { get; set; }

        public List<ShelfDto> Shelves { get; set; }

        public float Purity { get; set; }
        public string Remark { get; set; }

    }
}