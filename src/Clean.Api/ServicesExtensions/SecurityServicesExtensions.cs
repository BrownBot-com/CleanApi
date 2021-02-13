using Clean.Api.Security;
using Clean.Api.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.ServicesExtensions
{
    public static class SecurityServicesExtensions
    {
        public static void AddSecurityHelpers(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ISecurityContext, ApiSecurityContext>();
        }
    }
}
