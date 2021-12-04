using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cool.Api.Authentication;
using Cool.Api.ExceptionHandling;
using Cool.API.Extensions;
using Cool.Api.RequestContext;
using Cool.Bll.AccountService;
using Cool.Bll.CaffService;
using Cool.Bll.Mappings;
using Cool.Common.Options;
using Cool.Common.RequestContext;
using Cool.Dal;
using Cool.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using AuthenticationOptions = Cool.Common.Options.AuthenticationOptions;

namespace Cool.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringOptions = services.ConfigureOption<ConnectionStringOptions>(_configuration);

            services.Configure<AuthenticationOptions>(_configuration.GetSection(nameof(AuthenticationOptions)));

            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new Mappings()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(AuthenticationSchemes.JwtBearer)
                .AddScheme<AuthenticationSchemeOptions, JwtBearerAuthenticationHandler>(AuthenticationSchemes.JwtBearer, null);

            services.AddAuthorization();

            services.AddSwaggerDocument(c =>
                {
                    c.AddSecurity("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                        Enter 'Bearer' [space] and then your token in the text input below.
                                        \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
                });
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");
            services.AddHttpContextAccessor();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRequestContext, RequestContext>();
            services.AddScoped<ICaffService, CaffService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

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
