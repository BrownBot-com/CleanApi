using Clean.Api.DataAccess.Models;
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
    }
}
