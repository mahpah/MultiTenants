using System;
using System.ComponentModel.DataAnnotations;

namespace MultiTenants.Tenant.Model
{
    public class AppTenant
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
        public string TenantState { get; set; }
    }
}
