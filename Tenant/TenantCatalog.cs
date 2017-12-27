using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MultiTenants.Tenant.Interface;

namespace MultiTenants.Tenant
{
    public class TenantCatalog : ITenantCatalog
    {
        private readonly IEnumerable<AppTenant> db;

        public TenantCatalog(IConfiguration configuration)
        {
            db = configuration.GetTenantConfig();
        }

        public Task<AppTenant> Get(Func<AppTenant, bool> filterExp)
        {
            return Task.FromResult(db.Where(filterExp).FirstOrDefault());
        }
    }
}
