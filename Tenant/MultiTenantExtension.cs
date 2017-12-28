using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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
            services.AddScoped(prov => prov.GetService<IHttpContextAccessor>()?.HttpContext?.GetTenant());
            // services.AddScoped(prov => prov.GetService<TenantContext<TTenant>>()?.Tenant);

            // // Make ITenant injectable for handling null injection, similar to IOptions
            // services.AddScoped<ITenant<TTenant>>(prov => new TenantWrapper<TTenant>(prov.GetService<TTenant>()));
            return services;
        }
    }
}
