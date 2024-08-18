using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers.Base
{
    public class BaseController : Controller
    {
        protected string TenantId
        {
            get
            {
                var tenantId = HttpContext.Request.Cookies["TenantId"];
                return tenantId ?? 1.ToString();
            }
        }
    }
}
