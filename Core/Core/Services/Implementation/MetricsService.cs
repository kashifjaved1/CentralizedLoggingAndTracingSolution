using Core.Data;
using Core.Data.Entities;
using Core.Helpers;
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
    public class MetricsService : IMetricsService
    {
        private readonly ActivityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public MetricsService(ActivityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
        }

        public void LogMetric(string metricName, double value, string serviceName)
        {
            var metric = new Metric
            {
                TenantId = SessionHelper.GetTenantId(_httpContextAccessor),
                RequestId = SessionHelper.GetRequestId(_httpContextAccessor),
                ServiceName = _serviceName,
                MetricName = metricName,
                Value = $"{value} ms",
                Timestamp = DateTime.UtcNow
            };

            _context.Metrics.Add(metric);
            _context.SaveChanges();
        }
    }
}
