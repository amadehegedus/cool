using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cool.API.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool ShouldRunAngular(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return Environment.GetEnvironmentVariable("RUN_NG") == "true";
        }
    }
}
