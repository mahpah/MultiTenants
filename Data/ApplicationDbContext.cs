using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MultiTenants.Data.Models;
using MultiTenants.Tenant;
using MultiTenants.Tenant.Interface;

namespace MultiTenants.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Value> Values { get; set; }

        private readonly AppTenant _tenant;

        public ApplicationDbContext(ITenantResolver tenantResolver)
        {
            _tenant = tenantResolver.Resolve().GetAwaiter().GetResult();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _tenant.ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
