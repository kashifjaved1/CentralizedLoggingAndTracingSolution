using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class TracesController : Controller
    {
        private readonly ActivityDbContext _context;

        public TracesController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var traces = _context.Traces.Select(trace => new TraceViewModel
            {
                Id = trace.Id,
                RequestId = trace.RequestId,
                Message = trace.Message,
                Timestamp = trace.Timestamp
            }).ToList();

            return View(traces);
        }
    }

}
