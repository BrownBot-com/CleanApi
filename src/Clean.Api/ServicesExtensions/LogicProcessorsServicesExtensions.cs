using Clean.Api.LogicProcessors;
using Clean.Api.LogicProcessors.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.ServicesExtensions
{
    public static class LogicProcessorsServicesExtensions
    {
        public static void AddLogicProcessors(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationProcessor, AuthenticationProcessor>();
            services.AddScoped<IUsersProcessor, UsersProcessor>();
        }
    }
}
