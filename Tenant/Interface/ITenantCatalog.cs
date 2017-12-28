using System;
using System.Threading.Tasks;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant.Interface
{
    public interface ITenantCatalog
    {
        Task<AppTenant> Get(Func<AppTenant, bool> filterExp);
    }
}
