using Azure.Core;
using Core.Data;
using Core.Data.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implementation
{
    public class RequestResponseService : IRequestResponseService
    {
        private readonly ActivityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public RequestResponseService(ActivityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
        }

        public void LogRequest(Data.Entities.Request request)
        {
            request.ServiceName = _serviceName;
            request.RequestId = GetRequestId();
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        public void LogResponse(Response response)
        {
            response.RequestId = GetRequestId();
            _context.Responses.Add(response);
            _context.SaveChanges();
        }

        #region Private Methods

        private Guid GetRequestId()
        {
            if (_httpContextAccessor.HttpContext.Items.ContainsKey("RequestId"))
            {
                return (Guid)_httpContextAccessor.HttpContext.Items["RequestId"];
            }
            else
            {
                var requestId = Guid.NewGuid();
                _httpContextAccessor.HttpContext.Items["RequestId"] = requestId;
                return requestId;
            }
        }

        #endregion
    }
}
