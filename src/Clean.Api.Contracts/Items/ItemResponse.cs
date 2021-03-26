using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullCode { get; set; }
        public string OldCode { get; set; }

        public string Description { get; set; }

        public string FullDescription { get; set; }

        public string SupplierCode { get; set; }

        public string DiscountGroup { get; set; }
        public string StockGroup { get; set; }
        public string PriceListGroup { get; set; }
        public int PurchaseQty { get; set; }
        
        public string FullType { get; set; }

        public string BrandCode { get; set; }

        public string Errors { get; set; }

        public int DynamicsBatch { get; set; }

        public bool IsSoldInPacket { get; set; }

        public ItemStockResponse[] Stock { get; set; }
    }
}
