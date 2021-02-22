using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class Supplier
    {
        [Key]
        [Column("SupplierCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Code { get; set; }

        [Column("SupplierName")]
        [StringLength(128, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Name { get; set; }

    }
}
