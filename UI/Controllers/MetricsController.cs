using Microsoft.AspNetCore.Mvc;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class MetricsController : Controller
    {
        private readonly List<MetricsViewModel> _metrics;

        public MetricsController()
        {
            _metrics = new List<MetricsViewModel>
            {
                new MetricsViewModel { MetricName = "Sample Metric", Value = 123.45, Timestamp = DateTime.UtcNow }
            };
        }

        public IActionResult Index()
        {
            return View(_metrics);
        }
    }

}
