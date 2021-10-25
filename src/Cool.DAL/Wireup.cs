using System;
using System.Collections.Generic;
using System.Text;
using Cool.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cool.DAL
{
    public static class Wireup
    {
        public static void AddDAL(this IServiceCollection services, ConnectionStringOptions connectionStringOptions)
        {
            services.AddDbContext<CoolDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionStringOptions.DefaultConnection,
                    x => x.MigrationsAssembly("Cool.Dal"));
            });
        }
    }
}
