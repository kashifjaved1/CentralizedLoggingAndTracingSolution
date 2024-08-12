using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class TenantController : Controller
    {
        [HttpPost]
        public IActionResult ChangeTenant(Guid tenantId)
        {
            HttpContext.Session.SetString("TenantId", tenantId.ToString());
            return Ok();
        }
    }
}
