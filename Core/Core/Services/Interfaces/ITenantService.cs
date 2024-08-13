using Core.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Services.Interfaces
{
    public interface ITenantService
    {
        void SetTenant(HttpContext context);
        Task<List<Tenant>> GetTenants();
    }
}