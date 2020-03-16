using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestAspNetCoreWeb1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // added configuration
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services )
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW1: Incoming request");
                await context.Response.WriteAsync(Configuration["Message1"]);
                await next();
                logger.LogInformation("MW1: Outgoing response");
            });

            app.Use(async (context, next ) =>
            {
                logger.LogInformation("MW2: Incoming request");
                await context.Response.WriteAsync(Configuration["Message2"]);
                await next();
                logger.LogInformation("MW2: Outgoing response");
            });

            app.Run(async (context) =>
            {
                logger.LogInformation("MW3: Incoming request");
                await context.Response.WriteAsync(Configuration["Message3"]);
                logger.LogInformation("MW3: Outgoing response");
            });
        }
    }
}
