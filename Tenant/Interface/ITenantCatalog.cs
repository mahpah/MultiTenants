using System;
using System.Threading.Tasks;

namespace MultiTenants.Tenant.Interface
{
    public interface ITenantCatalog
    {
        Task<AppTenant> Get(Func<AppTenant, bool> filterExp);
    }
}
