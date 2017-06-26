using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Common;
using System;

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

        //TEMPORARY  TODO

        private const string SecretKey = "needtogetthisfromenvironment";

        //sdkfnasdjkfnsdfmaskf TODO ACKNOWLEDGE ME PLS

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            
            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            

            UpstreamDataAccess u = new UpstreamDataAccess();
            ServersDataAccess s = new ServersDataAccess();
            ProxyTypeDataAccess p = new ProxyTypeDataAccess();
            VirtualServerDataAccess vs = new VirtualServerDataAccess();
            SSLDataAccess ssl = new SSLDataAccess();
            ApplicationDataAccess app = new ApplicationDataAccess();
            LocationsDataAccess l = new LocationsDataAccess();
            GeneralConfigDataAccess gc = new GeneralConfigDataAccess();
            DeploymentServerDataAccess ds = new DeploymentServerDataAccess();
            UserDataAccess us = new UserDataAccess();
            BlacklistDataAccess b = new BlacklistDataAccess();


            services.AddSingleton<IRepository<User>>(us);
            services.AddSingleton<IRepository<Upstream>>(u);
            services.AddSingleton<IRepository<Server>>(s);
            services.AddSingleton<IRepository<ProxyType>>(p);
            services.AddSingleton<IRepository<VirtualServer>>(vs);
            services.AddSingleton<IRepository<SSL>>(ssl);
            services.AddSingleton<IRepository<Application>>(app);
            services.AddSingleton<IRepository<Location>>(l);
            services.AddSingleton<IRepository<GeneralConfig>>(gc);
            services.AddSingleton<IRepository<DeploymentServer>>(ds);
            services.AddSingleton<IRepository<Blacklist>>(b);



            services.AddSingleton(new AllRepositories(s, u, p, vs, ssl, app,l,gc, ds, b));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();  

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseWebSockets();
            app.UseMiddleware<WebSMiddleware>();

            app.UseMvc();
        }
    }
}
