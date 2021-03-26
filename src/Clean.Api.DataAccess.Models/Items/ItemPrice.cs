using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class ItemPrice
    {
        [Key]
        [Column("ItemPriceId")]
        public int Id { get; set; }

        [Column("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ItemCode")]
        [StringLength(50, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string ItemCode { get; set; }

        [Column("ItemPriceUnitPrice")]
        public double UnitPrice { get; set; }

        [Column("ItemPriceUnitCost")]
        public double UnitCost { get; set; }

        [Column("ItemPriceIncludesGST")]
        public bool PriceIncludesGST{ get; set; }

        [Column("ItemPriceDate")]
        public DateTime Date { get; set; }

        [Column("PriceListId")]
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }
    }
}
