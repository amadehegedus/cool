using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cool.API.Extensions;
using Cool.Common.Options;
using Cool.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cool.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _environment = hostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringOptions = services.ConfigureOption<ConnectionStringOptions>(_configuration);

            //P�lda tov�bbi options injekt�l�s�ra
            //services.Configure<TodoOptions>(_configuration.GetSection(nameof(TodoOptions)));

            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");
            services.AddHttpContextAccessor();

            services.AddDAL(connectionStringOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../Cool.Web";

                if (env.IsDevelopment())
                {
                    if (env.ShouldRunAngular())
                    {
                        spa.Options.StartupTimeout = new TimeSpan(0, 3, 0);
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                    else
                    {
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    }
                }
            });
        }
    }
}