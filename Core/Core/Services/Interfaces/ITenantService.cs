using Microsoft.AspNetCore.Http;

namespace Core.Services.Interfaces
{
    public interface ITenantService
    {
        void SetTenant(HttpContext context);
    }
}