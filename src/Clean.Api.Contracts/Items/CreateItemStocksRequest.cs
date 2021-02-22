using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreateItemStocksRequest
    {
        CreateItemStockRequest[] Items { get; set; } = new CreateItemStockRequest[0];
    }

    public class CreateItemStockRequest
    {
        [Required]
        public string ItemCode { get; set; }

        [Required]
        public string BranchCode { get; set; }

        [Required]
        public DateTime LastOrdered { get; set; }

        [Required]
        public int Min { get; set; }

        [Required]
        public int Max { get; set; }

        [Required]
        public int Current { get; set; }

    }
}
