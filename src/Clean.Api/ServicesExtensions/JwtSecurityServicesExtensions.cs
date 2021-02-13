using Clean.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.ServicesExtensions
{
    public static class JwtSecurityServicesExtensions
    {
        public static void AddJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (o) =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = TokenAuthOptions.Key,
                        ValidAudience = TokenAuthOptions.Audience,
                        ValidIssuer = TokenAuthOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
