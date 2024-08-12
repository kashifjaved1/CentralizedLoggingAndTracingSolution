using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class TracesController : BaseController
    {
        private readonly ActivityDbContext _context;

        public TracesController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var traces = _context
                .Traces
                .OrderByDescending(t => t.Timestamp)
                .Select(t => new TraceViewModel
            {
                Id = t.Id,
                RequestId = t.RequestId,
                Message = t.Message,
                Timestamp = t.Timestamp
            }).ToList();

            return View(traces);
        }
    }

}
