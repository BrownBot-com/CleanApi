using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreateItemPriceRequest
    {
        public string ItemCode { get; set; }

        public double UnitPrice { get; set; }

        public double UnitCost { get; set; }

        public bool PriceIncludesGST { get; set; }
    }
}
