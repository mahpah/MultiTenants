using System.Threading.Tasks;

namespace MultiTenants.Tenant.Interface
{
    public interface ITenantResolver
    {
        Task<AppTenant> Resolve();
    }
}
