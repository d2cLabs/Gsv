using System.Collections.Generic;
using Gsv.Objects;

namespace Gsv.Web.Models.Weixin
{
    public class ListViewModel
    {
        public string PlaceInfo { get; set; }

        public string Collateral { get; set; }

        public string TodaySummary { get; set; }

        public List<ItemInfo> Items { get; set; }


        public int ShelfId { get; set; }

        public List<Shelf> Shelves { get; set; }
    }
}