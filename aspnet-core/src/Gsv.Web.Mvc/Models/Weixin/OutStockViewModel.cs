﻿using System.Collections.Generic;
using Gsv.Objects.Dto;
using Gsv.Types.Dto;

namespace Gsv.Web.Models.Weixin
{
    public class OutStockViewModel
    {
        public int ShelfId { get; set; }

        public List<ShelfDto> Shelves { get; set; }

        public float Quantity { get; set; }

    }
}