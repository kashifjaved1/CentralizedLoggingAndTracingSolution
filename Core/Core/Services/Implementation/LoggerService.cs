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
using Core.UOW;

namespace Core.Services.Implementation
{
    public class LoggerService : ILoggerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public LoggerService(IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
            _uow = uow;
        }

        public async void LogInformation(string message)
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

            await _uow.Repository<Log>().AddAsync(log);
            //_context.Logs.Add(log);
            //await _context.SaveChangesAsync();
        }

        public async void LogError(string message)
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

            await _uow.Repository<Log>().AddAsync(log);
            //_context.Logs.Add(log);
            //await _context.SaveChangesAsync();
        }

        public async void Trace(string message)
        {
            var trace = new Trace
            {
                TenantId = SessionHelper.GetTenantId(_httpContextAccessor),
                RequestId = SessionHelper.GetRequestId(_httpContextAccessor),
                Message = $"{_serviceName}: {message}",
                Timestamp = DateTime.UtcNow
            };

            await _uow.Repository<Trace>().AddAsync(trace);
            //_context.Traces.Add(trace);
            //await _context.SaveChangesAsync();
        }
    }

}
