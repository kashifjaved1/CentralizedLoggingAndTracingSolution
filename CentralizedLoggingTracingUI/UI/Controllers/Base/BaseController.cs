using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected int TenantId
        {
            get
            {
                var tenantId = HttpContext.Request.Cookies["TenantId"];
                return tenantId != null ? int.Parse(tenantId) : 1;
            }
        }
    }
}
