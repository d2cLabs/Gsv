﻿using System.Collections.Generic;
using Gsv.Objects.Dto;
using Gsv.Types.Dto;

namespace Gsv.Web.Models.Weixin
{
    public class AllotViewModel
    {
        public int ObjectId { get; set; }
        public int FromShelfId { get; set; }
        public int ToShelfId { get; set; }

        public List<ShelfDto> Shelves { get; set; }

        public double Quantity { get; set; }

        public string Remark { get; set; }
    }
}