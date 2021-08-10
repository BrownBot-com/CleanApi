using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreateItemPriceRequest
    {
        public string ItemCode { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public double UnitCost { get; set; }

        public int UnitQty { get; set; } = 1;

        public bool PriceIncludesGST { get; set; } = false;

        public string StockGroup { get; set; }
    }
}
