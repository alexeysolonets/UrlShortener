using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Bll.Model;
using Autofac;
using UrlShortener.Config.Autofac;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UrlShortener
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
					// http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services
					.AddControllersAsServices();
            
            services.AddDbContext<UrlsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("UrlsDatabase"));
            });
        }

        // Configure Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ControllersModule>();
            builder.RegisterModule<CqrsModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseDefaultFiles();
            app.UseStaticFiles();
			app.UseMvc();
        }
    }
}
