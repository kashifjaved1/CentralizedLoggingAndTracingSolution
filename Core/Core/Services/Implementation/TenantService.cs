using Core.Data;
using Microsoft.AspNetCore.Http;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly ActivityDbContext _context;

        public TenantService(ActivityDbContext context)
        {
            _context = context;
        }

        public async void SetTenant(HttpContext httpContext)
        {
            var tenantId = httpContext.Request.Cookies["TenantId"];

            if (!string.IsNullOrEmpty(tenantId))
            {
                var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Identifier == tenantId);
                if (tenant != null)
                {
                    httpContext.Items["TenantId"] = tenant.Id.ToString();
                }
                else
                {
                    throw new Exception("Invalid tenant");
                }
            }
        }
    }
}
