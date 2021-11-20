using Cool.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cool.Dal
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
