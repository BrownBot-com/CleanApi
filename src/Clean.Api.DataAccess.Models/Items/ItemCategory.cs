using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    [Table("ItemCategory")]
    public class ItemCategory
    {
        [Key]
        [Column("ItemCatCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Code { get; set; }

        [Column("ItemCatDescription")]
        [StringLength(128, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Description { get; set; }

        [Column("ItemCatNumber")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Number { get; set; }
    }
}
