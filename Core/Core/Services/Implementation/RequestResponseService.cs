using Azure.Core;
using Core.Data;
using Core.Data.Entities;
using Core.Helpers;
using Core.Services.Interfaces;
using Core.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implementation
{
    public class RequestResponseService : IRequestResponseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public RequestResponseService(IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
            _uow = uow;
        }

        public async void LogRequest(Data.Entities.Request request)
        {
            request.ServiceName = _serviceName;
            request.RequestId = SessionHelper.GetRequestId(_httpContextAccessor);
            request.TenantId = SessionHelper.GetTenantId(_httpContextAccessor);

            await _uow.Repository<Data.Entities.Request>().AddAsync(request);
            //_context.Requests.Add(request);
            //await _context.SaveChangesAsync();
        }

        public async void LogResponse(Response response)
        {
            response.RequestId = SessionHelper.GetRequestId(_httpContextAccessor);
            response.TenantId = SessionHelper.GetTenantId(_httpContextAccessor);

            await _uow.Repository<Response>().AddAsync(response);
            //_context.Responses.Add(response);
            //await _context.SaveChangesAsync();
        }
    }
}
