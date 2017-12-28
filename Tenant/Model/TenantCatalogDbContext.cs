using Microsoft.EntityFrameworkCore;

namespace MultiTenants.Tenant.Model
{
    public class TenantCatalogDbContext : DbContext
    {
        public DbSet<AppTenant> AppTenants { get; set; }

        public TenantCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
