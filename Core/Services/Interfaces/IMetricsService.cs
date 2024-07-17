using Core.Data.Entities;

namespace Core.Services.Interfaces
{
    public interface IMetricsService
    {
        void LogMetric(string metricName, double value, string serviceName);
    }
}