using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreateItemRequest
    {
        [Required]
        public string Code { get; set; }

        public string OldCode { get; set; }

        public string FullDescription { get; set; }

        public string SupplierCode { get; set; }

        public string DiscountGroup { get; set; }
        public string StockGroup { get; set; }
        public string PriceListGroup { get; set; }
        public int PurchaseQty { get; set; }

        public string BrandCode { get; set; }

        public string FullType { get; set; }
    }
}
