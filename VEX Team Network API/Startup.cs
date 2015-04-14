using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using VexTeamNetwork.Api.Models;

namespace VexTeamNetwork.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<VexTeamNetContext>(options => options.UseSqlServer(Configuration.Get("Data:IdentityConnection:ConnectionString")));
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = ClaimsIdentityOptions.DefaultAuthenticationType,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
    }
