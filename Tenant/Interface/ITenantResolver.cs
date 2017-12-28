using System.Threading.Tasks;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant.Interface
{
    public interface ITenantResolver
    {
        Task<AppTenant> Resolve();
    }
}
