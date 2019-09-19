using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackOPS.App_Start;
using BlackOPS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI2.App_Start;

namespace BlackOPS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfig.AddScope(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ConfigurationManager>(Configuration.GetSection("ConfigurationManager"));
            JwtTokenConfig.AddAuthentication(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseMvc();
            if (env.IsDevelopment())

            {

                app.UseDeveloperExceptionPage();

            }

            app.UseCors(builder => builder

                .AllowAnyOrigin()

                .AllowAnyMethod()

                .AllowAnyHeader()

                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
