using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Helpers
{
    public static class SessionHelper
    {
        public static Guid GetRequestId(IHttpContextAccessor httpContextAccessor)
        {
            var httpContextItems = httpContextAccessor?.HttpContext?.Items;
            var isRequestIdAvailable = httpContextItems.ContainsKey("RequestId");
            if (!string.IsNullOrEmpty(isRequestIdAvailable.ToString()) && isRequestIdAvailable)
            {
                return (Guid)httpContextItems["RequestId"];
            }
            else
            {
                var requestId = Guid.NewGuid();
                httpContextItems["RequestId"] = requestId;
                return requestId;
            }
        }

        public static string GetTenantId(IHttpContextAccessor httpContextAccessor)
        {
            //var tenantId = httpContextAccessor?.HttpContext?.Session.GetString("TenantId");
            var tenantId = httpContextAccessor?.HttpContext?.Request.Cookies["TenantId"];
            if (string.IsNullOrEmpty(tenantId))
            {
                return 1.ToString(); // The default tenant;
            }

            return tenantId;
        }
    }
}
