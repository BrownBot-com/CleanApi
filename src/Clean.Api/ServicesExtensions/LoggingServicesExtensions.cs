using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.ServicesExtensions
{
    public static class LoggingServicesExtensions
    {
        public static void AddLogging(this IServiceCollection services, IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.EnvironmentName == "Development")
            {
                //for local dev, do not need to use AzureBlobStorage
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .WriteTo.File("Logs/log.log", rollOnFileSizeLimit: true, fileSizeLimitBytes: 500000, shared: true)
                    .CreateLogger();
                Log.Information("Logging configured for Environment = '{0}'.", hostingEnvironment.EnvironmentName);
            }
        }
    }
}
