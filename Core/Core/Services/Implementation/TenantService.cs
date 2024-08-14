using Core.Data;
using Microsoft.AspNetCore.Http;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Core.Data.Entities;
using Core.Helpers;
using Core.UOW;
using System.ComponentModel;

namespace Core.Services.Implementation
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _uow;

        public TenantService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Tenant>> GetTenantsAsync()
        {
            return await _uow.Repository<Tenant>().GetAllOrderedAsync(x => x.Identifier != "default", x => x.Id);
        }

        public async Task<bool> IsSelectedTenantAsync(string tenantIdFromCookie)
        {
            if (string.IsNullOrEmpty(tenantIdFromCookie))
            {
                return false;
            }

            var tenantId = int.Parse(tenantIdFromCookie);
            return await _uow.Repository<Tenant>().GetByIdAsync(tenantId) is not null;
        }

        public async void SetTenant(HttpContext httpContext)
        {
            //var tenantId = httpContext.Session.GetString("TenantId"); // when application will restart, then it'll be null.
            var tenantId = httpContext.Request.Cookies["TenantId"];

            if (!string.IsNullOrEmpty(tenantId))
            {
                var id = int.Parse(tenantId);
                var tenant = await _uow.Repository<Tenant>().GetByIdAsync(id);
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
