using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class TenantController : Controller
    {
        [HttpPost]
        public IActionResult ChangeTenant(int tenantId)
        {
            HttpContext.Session.SetString("TenantId", tenantId.ToString());
            HttpContext.Response.Cookies.Append("TenantId", tenantId.ToString(), new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true,
                IsEssential = true
            });
            return Ok();
        }
    }
}
