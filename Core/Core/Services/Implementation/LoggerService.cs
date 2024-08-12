using Core.Data.Entities;
using Core.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Core.Helpers;

namespace Core.Services.Implementation
{
    public class LoggerService : ILoggerService
    {
        private readonly ActivityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public LoggerService(ActivityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
        }

        public void LogInformation(string message)
        {
            var log = new Log
            {
                TenantId = SessionHelper.GetTenantId(_httpContextAccessor),
                RequestId = SessionHelper.GetRequestId(_httpContextAccessor),
                ServiceName = _serviceName ?? string.Empty,
                Message = $"{_serviceName}: {message}",
                LogLevel = "Information",
                Timestamp = DateTime.UtcNow
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void LogError(string message)
        {
            var log = new Log
            {
                TenantId = SessionHelper.GetTenantId(_httpContextAccessor),
                RequestId = SessionHelper.GetRequestId(_httpContextAccessor),
                ServiceName = _serviceName ?? string.Empty,
                Message = $"{_serviceName}: {message}",
                LogLevel = "Error",
                Timestamp = DateTime.UtcNow
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void Trace(string message)
        {
            var trace = new Trace
            {
                TenantId = SessionHelper.GetTenantId(_httpContextAccessor),
                RequestId = SessionHelper.GetRequestId(_httpContextAccessor),
                Message = $"{_serviceName}: {message}",
                Timestamp = DateTime.UtcNow
            };
            _context.Traces.Add(trace);
            _context.SaveChanges();
        }
    }

}
