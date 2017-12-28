using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenants.Tenant;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Utils
{
    public class AppInitUtils
    {
        private readonly TenantCatalogDbContext _tenantCatalogContext;
        private readonly IConfiguration _configuration;

        public AppInitUtils(TenantCatalogDbContext tenantCatalogContext, IConfiguration configuration)
        {
            _tenantCatalogContext = tenantCatalogContext;
            _configuration = configuration;
        }

        public async Task SeedTenant()
        {
            _tenantCatalogContext.Database.Migrate();

            if (_tenantCatalogContext.AppTenants.Any())
                return;

            var defaultTenants = _configuration.GetTenantConfig();
            await _tenantCatalogContext.AppTenants.AddRangeAsync(defaultTenants);
            await _tenantCatalogContext.SaveChangesAsync();
        }
    }
}
