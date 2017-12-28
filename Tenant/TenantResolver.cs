using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MultiTenants.Tenant.Interface;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantCatalog _tenantCatalog;

        public TenantResolver(IHttpContextAccessor httpContextAccessor, ITenantCatalog tenantCatalog)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantCatalog = tenantCatalog;
        }

        public async Task<AppTenant> Resolve()
        {
            var hostname = _httpContextAccessor.HttpContext.Request.Host.Value;
            var tenant = await _tenantCatalog.Get(t => t.HostName == hostname);
            return tenant;
        }
    }
}
