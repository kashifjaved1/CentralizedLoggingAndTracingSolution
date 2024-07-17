using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class MetricsController : Controller
    {
        private readonly ActivityDbContext _context;

        public MetricsController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var metrics = _context.Metrics
                .Select(m => new MetricViewModel
                {
                    Id = m.Id,
                    RequestId = m.RequestId,
                    ServiceName = m.ServiceName,
                    MetricName = m.MetricName,
                    Value = m.Value,
                    Timestamp = m.Timestamp
                })
                .ToList();
            return View(metrics);
        }

        public IActionResult Details(int id)
        {
            var metric = _context.Metrics
                .Where(m => m.Id == id)
                .Select(m => new MetricViewModel
                {
                    Id = m.Id,
                    RequestId = m.RequestId,
                    ServiceName = m.ServiceName,
                    MetricName = m.MetricName,
                    Value = m.Value,
                    Timestamp = m.Timestamp
                })
                .FirstOrDefault();

            if (metric == null)
            {
                return NotFound();
            }

            return View(metric);
        }
    }


}
