using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Users
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }

        [Column("UserUsername")]
        public string Username { get; set; }

        [Column("UserPassword")]
        public string PasswordHash { get; set; }

        [Column("UserFirstName")]
        public string FirstName { get; set; }

        [Column("UserLastName")]
        public string LastName { get; set; }

        [Column("UserIsDeleted")]
        public bool IsDeleted { get; set; }

        public virtual List<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
