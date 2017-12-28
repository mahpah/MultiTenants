using Microsoft.AspNetCore.Http;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant
{
    public static class HttpContextExtensions
    {
        private const string _key = "tenant";

        public static void SetTenant(this HttpContext httpContext, AppTenant tenant)
        {
            httpContext.Items[_key] = tenant;
        }

        public static AppTenant GetTenant(this HttpContext httpContext)
        {
            object tenantContext;
            if (httpContext.Items.TryGetValue(_key, out tenantContext))
            {
                return tenantContext as AppTenant;
            }

            return default(AppTenant);
        }
    }
}
