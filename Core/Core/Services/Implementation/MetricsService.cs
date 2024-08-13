using Core.Data.Entities;
using Core.Helpers;
using Core.Services.Interfaces;
using Core.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services.Implementation
{
    public class MetricsService : IMetricsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _serviceName;

        public MetricsService(IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceName = _httpContextAccessor.HttpContext?.RequestServices.GetService<IWebHostEnvironment>()?.ApplicationName ?? "UnknownService";
            _uow = uow;
        }

        public async void LogMetric(string metricName, double value, string serviceName)
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

            await _uow.Repository<Metric>().AddAsync(metric);
            //_context.Metrics.Add(metric);
            //await _context.SaveChangesAsync();
        }
    }
}
