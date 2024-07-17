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
                RequestId = GetRequestId(),
                ServiceName = _serviceName,
                MetricName = metricName,
                Value = $"{value} ms",
                Timestamp = DateTime.UtcNow
            };

            _context.Metrics.Add(metric);
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
