using Microsoft.AspNetCore.Http;
using System.Text;

namespace Core.Helpers
{
    public static class SessionHelper
    {
        public static Guid GetRequestId(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.Items.ContainsKey("RequestId"))
            {
                return (Guid)httpContextAccessor.HttpContext.Items["RequestId"];
            }
            else
            {
                var requestId = Guid.NewGuid();
                httpContextAccessor.HttpContext.Items["RequestId"] = requestId;
                return requestId;
            }
        }

        public static string GetTenantId(IHttpContextAccessor httpContextAccessor)
        {
            var tenantId = httpContextAccessor?.HttpContext?.Session.GetString("TenantId");
            if (string.IsNullOrEmpty(tenantId))
            {
                return Guid.Empty.ToString();
            }

            return tenantId;
        }
    }
}
