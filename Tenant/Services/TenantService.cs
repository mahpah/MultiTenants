using System.Threading.Tasks;
using AutoMapper;
using MultiTenants.Tenant.Constants;
using MultiTenants.Tenant.Dto;
using MultiTenants.Tenant.Interface;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Tenant.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantCatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public TenantService(TenantCatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AppTenantDto> CreateTenant(AppTenantDto tenantDto)
        {
            var tenant = _mapper.Map<AppTenant>(tenantDto);
            tenant.TenantState = TenantState.Initializing;
            var entry = await _dbContext.AddAsync(tenant);
            await _dbContext.SaveChangesAsync();

            // Perform provisioning
            return _mapper.Map<AppTenantDto>(tenant);
        }
    }
}
