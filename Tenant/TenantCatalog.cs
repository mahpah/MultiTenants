using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MultiTenants.Tenant.Interface;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant
{
    public class TenantCatalog : ITenantCatalog
    {
        private readonly IEnumerable<AppTenant> _db;

        public TenantCatalog(IConfiguration configuration)
        {
            _db = configuration.GetTenantConfig();
        }

        public Task<AppTenant> Get(Func<AppTenant, bool> filterExp)
        {
            return Task.FromResult(_db.Where(filterExp).FirstOrDefault());
        }
    }
}
