using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected Guid TenantId
        {
            get
            {
                var tenantId = HttpContext.Session.GetString("TenantId");
                return tenantId != null ? Guid.Parse(tenantId) : Guid.Empty;
            }
        }
    }
}
