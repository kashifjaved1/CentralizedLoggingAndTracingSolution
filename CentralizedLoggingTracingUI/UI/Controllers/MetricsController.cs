using Core.Data;
using Core.Data.Entities;
using Core.UOW;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class MetricsController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public MetricsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var metrics = _uow.Repository<Metric>()
                .GetAllOrdered(metric => metric.TenantId == TenantId, metric => metric.Timestamp, true)
                .Select(metric => new MetricViewModel
                {
                    Id = metric.Id,
                    RequestId = metric.RequestId,
                    ServiceName = metric.ServiceName,
                    MetricName = metric.MetricName,
                    Value = metric.Value,
                    Timestamp = metric.Timestamp
                })
                .ToList();
            return View(metrics);
        }

        public IActionResult Details(int id)
        {
            var metric = _uow.Repository<Metric>()
                .GetById(metric => metric.Id == id && metric.TenantId == TenantId);

            var metricViewModel = new MetricViewModel
            {
                Id = metric.Id,
                RequestId = metric.RequestId,
                ServiceName = metric.ServiceName,
                MetricName = metric.MetricName,
                Value = metric.Value,
                Timestamp = metric.Timestamp
            };

            if (metric == null)
            {
                return NotFound();
            }

            return View(metric);
        }
    }


}
