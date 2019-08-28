using System.Collections.Generic;
using Gsv.Objects.Dto;

namespace Gsv.Web.Models.Weixin
{
    public class StocktakingViewModel
    {
        public int ShelfId { get; set; }

        public List<ShelfDto> Shelves { get; set; }

        public float Inventory { get; set; }
    }
}