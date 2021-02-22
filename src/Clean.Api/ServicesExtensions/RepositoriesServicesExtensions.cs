using Clean.Api.Data.Access;
using Clean.Api.DataAccess.EF;
using Clean.Api.DataAccess.Models.Interfaces;
using Clean.Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Api.DataAccess.Models.Users;
using Clean.Api.DataAccess.Models.Items;

namespace Clean.Api.ServicesExtensions
{
    public static class RepositoriesServicesExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectString = configuration.GetConnectionString("clean-api-db");
            services.AddDbContext<CleanDbContext>(options => options.UseSqlServer(connectString));

            services.AddScoped<IRepository<User>>(x => new EFRepository<User>(x.GetRequiredService<CleanDbContext>()));
            services.AddScoped<IRepository<Item>>(x => new EFRepository<Item>(x.GetRequiredService<CleanDbContext>()));
        }
    }
}
