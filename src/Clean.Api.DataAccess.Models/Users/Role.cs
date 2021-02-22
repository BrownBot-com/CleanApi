using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Users
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [Column("RoleId")]
        public int Id { get; set; }

        [Column("RoleName")]
        public string Name { get; set; }

        [Column("RoleAuthority")]
        public int Authority { get; set; }
    }
}
