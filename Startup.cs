using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenants.Data;
using MultiTenants.Tenant;
using MultiTenants.Tenant.Model;
using MultiTenants.Utils;

namespace MultiTenants
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMultiTenantsSupport();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>();

            services.AddDbContext<TenantCatalogDbContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("TenantCatalog")));
            services.AddMvc();
            services.AddTransient<AppInitUtils>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
