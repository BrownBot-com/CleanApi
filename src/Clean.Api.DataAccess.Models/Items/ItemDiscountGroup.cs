using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    [Table("ItemDiscountGroup")]
    public class ItemDiscountGroup
    {
        [Key]
        [Column("ItemDiscGrpCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Code { get; set; }

        [Column("ItemDiscGrpDescription")]
        [StringLength(128, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Description { get; set; }

        [Column("ItemDiscGrpAdditional")]
        [StringLength(128, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Additional { get; set; }

        [Column("ItemDiscGrpSupplierRef")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string SupplierRef { get; set; }

        [Column("ItemDiscGrpItemCategory")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string ItemCategory { get; set; }
    }
}
