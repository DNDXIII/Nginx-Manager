using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            UpstreamDataAccess u = new UpstreamDataAccess();
            ServersDataAccess s = new ServersDataAccess();
            ProxyTypeDataAccess p = new ProxyTypeDataAccess();
            VirtualServerDataAccess vs = new VirtualServerDataAccess();

            services.AddSingleton<IRepository<Upstream>>(u);
            services.AddSingleton<IRepository<Server>>(s);
            services.AddSingleton<IRepository<ProxyType>>(p);
            services.AddSingleton<IRepository<VirtualServer>>(vs);


            services.AddSingleton<AllRepositories>(new AllRepositories(s, u, p));

            // Add framework services.
            services.AddMvc();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
