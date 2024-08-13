using Core.Data;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Middlewares
{
    public class TenantMiddleware : IMiddleware
    {
        private readonly ITenantService _tenantService;

        public TenantMiddleware(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await _tenantService.SetTenant(context);
            
            await next(context);
        }
    }

}
