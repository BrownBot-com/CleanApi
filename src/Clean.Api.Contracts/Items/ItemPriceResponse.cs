using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class ItemPriceResponse
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string Description { get; set; }

        public string StockGroup { get; set; }

        public double UnitPrice { get; set; }

        public double UnitCost { get; set; }

        public int UnitQty { get; set; }

        public bool PriceIncludesGST { get; set; }

        public DateTime Date { get; set; }
    }
}
