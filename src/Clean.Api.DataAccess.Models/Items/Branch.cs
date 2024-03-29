﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class Branch
    {
        [Key]
        [Column("BranchCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Code { get; set; }

        [Column("BranchName")]
        [StringLength(128, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string Name { get; set; }

        [Column("BranchNewCode")]
        [StringLength(20, ErrorMessage = "The {0} value exceeds {1} characters")]
        public string NewCode { get; set; }
    }
}
