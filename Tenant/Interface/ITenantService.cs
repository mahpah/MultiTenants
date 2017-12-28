using System.Threading.Tasks;
using MultiTenants.Tenant.Dto;

namespace MultiTenants.Tenant.Interface
{
    public interface ITenantService
    {
        Task<AppTenantDto> CreateTenant(AppTenantDto tenantDto);
    }
}
