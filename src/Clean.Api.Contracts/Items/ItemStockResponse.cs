﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class ItemStockResponse
    {
        public int Id { get; set; }

        public string BranchCode { get; set; }

        public int ItemId { get; set; }
        
        public string ItemCode { get; set; }

        public DateTime LastOrdered { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public int Current { get; set; }

        public string Bin { get; set; }

        public bool Reprocess { get; set; }
    }
}
