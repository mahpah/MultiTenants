using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MultiTenants.Tenant.Interface;

namespace MultiTenants.Tenant
{
    public class TenantResolveMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TenantResolveMiddleware> _log;

        public TenantResolveMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _log = loggerFactory.CreateLogger<TenantResolveMiddleware>();
        }

        public async Task Invoke(HttpContext context, ITenantResolver tenantResolver)
        {
            var tenant = await tenantResolver.Resolve();
            if (tenant != null)
            {
                // should wrap tenant into a disposable object?
                context.SetTenant(tenant);
            }
            else
            {
                _log.LogDebug("Cannot resolve tenant");
            }

            await this._next.Invoke(context);
        }
    }
}
