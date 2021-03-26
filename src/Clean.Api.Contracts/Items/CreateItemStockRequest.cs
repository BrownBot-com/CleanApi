using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clean.Api.Contracts.Items
{

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

        public int ImportNumber { get; set; }

        public string Bin { get; set; }
    }
}
