using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class Item
    {
        [Key]
        [Column("ItemId")]
        public int Id { get; set; }

        [Column("ItemCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Code { get; set; }

        [Column("ItemFullCode")]
        public string FullCode { get; set; }

        [Column("ItemOldCode")]
        public string OldCode { get; set; }

        [Column("ItemDescription")]
        [StringLength(100, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Description { get; set; }

        [Column("ItemFullDescription")]
        public string FullDescription { get; set; }

        [Column("ItemDiscountGroup")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string DiscountGroup { get; set; }

        [Column("ItemStockGroup")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string StockGroup { get; set; }

        [Column("ItemPriceListGroup")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string PriceListGroup { get; set; }

        [Column("ItemPurchaseQty")]
        public int PurchaseQty { get; set; }

        [Column("SupplierCode")]
        public string SupplierCode { get; set; }
        public Supplier Supplier { get; set; }

        [Column("BrandCode")]
        public string BrandCode { get; set; }
        public Brand Brand { get; set; }

        [Column("ItemFullType")]
        [StringLength(50, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string FullType { get; set; }

        [Column("ItemDynamicsBatch")]
        public int DynamicsBatch { get; set; }

        [Column("ItemSoldInPacket")]
        public bool IsSoldInPacket{ get; set; }


        [Column("ItemErrors")]
        public string Errors { get; set; }

        public List<ItemStock> Stock { get; set; } = new List<ItemStock>();
        
        public List<ItemPrice> Prices { get; set; } = new List<ItemPrice>();
    }
}
