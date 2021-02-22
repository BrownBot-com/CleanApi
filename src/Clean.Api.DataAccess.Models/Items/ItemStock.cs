﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class ItemStock
    {
        [Key]
        [Column("ItemStockId")]
        public int Id { get; set; }

        [Column("BranchCode")]
        public string BranchCode { get; set; }
        public Branch Branch { get; set; }

        [Column("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ItemStockLastOrdered")]
        public DateTime LastOrdered { get; set; }

        [Column("ItemStockMin")]
        public int Min { get; set; }

        [Column("ItemStockMax")]
        public int Max { get; set; }

        [Column("ItemStockCurrent")]
        public int Current { get; set; }


    }
}
