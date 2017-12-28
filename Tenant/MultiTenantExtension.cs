using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenants.Tenant.Interface;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant
{
    public static class MultiTenantExtension
    {
        public static IEnumerable<AppTenant> GetTenantConfig(this IConfiguration configuration)
        {
            var tenants = new List<AppTenant>();
            configuration.GetSection("Tenants").Bind(tenants);
            return tenants;
        }

        public static IServiceCollection AddMultiTenantsSupport(this IServiceCollection services)
        {
            services.AddSingleton<ITenantCatalog, TenantCatalog>();
            services.AddScoped<ITenantResolver, TenantResolver>();
            return services;
        }
    }
}
