using Core.Data;
using Microsoft.AspNetCore.Http;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Core.Data.Entities;
using Core.Helpers;

namespace Core.Services.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly ActivityDbContext _context;

        public TenantService(ActivityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tenant>> GetTenants()
        {
            return await _context.Tenants.Where(x => !StringHelper.AreEquals(x.Identifier, "default")).OrderBy(x => x.Id).ToListAsync();
        }

        public async void SetTenant(HttpContext httpContext)
        {
            //var abc = httpContext.Session.GetString("TenantId"); // when application will restart, then it'll be null.
            var tenantId = httpContext.Request.Cookies["TenantId"];

            if (!string.IsNullOrEmpty(tenantId))
            {
                var id = int.Parse(tenantId);
                var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);
                if (tenant != null)
                {
                    httpContext.Items["TenantId"] = tenant.Id.ToString();
                }
                else
                {
                    throw new Exception("Invalid tenant");
                }
            }
            else
            {
                httpContext.Items["TenantId"] = 1.ToString(); // setting default tenant if no tenant is selected.
            }
        }
    }
}
