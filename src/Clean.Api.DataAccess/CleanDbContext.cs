using Clean.Api.DataAccess.Models.Items;
using Clean.Api.DataAccess.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Data.Access
{
    public class CleanDbContext : DbContext
    {
        public CleanDbContext(DbContextOptions<CleanDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<UserRole> UserRoles { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemStock> ItemStocks { get; set; }
        public virtual DbSet<PriceList> PriceLists { get; set; }
    }
}
