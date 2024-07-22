using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class LogsController : Controller
    {
        private readonly ActivityDbContext _context;

        public LogsController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var logs = _context
                .Logs
                .OrderByDescending(log => log.Timestamp)
                .Select(log => new LogViewModel
            {
                Id = log.Id,
                RequestId = log.RequestId,
                Message = log.Message,
                LogLevel = log.LogLevel,
                Timestamp = log.Timestamp
            }).ToList();

            return View(logs);
        }

        public IActionResult Details(Guid requestId)
        {
            var logs = _context.Logs
                .Where(log => log.RequestId == requestId)
                .OrderByDescending(log => log.Timestamp)
                .Select(log => new LogViewModel
                {
                    Id = log.Id,
                    Message = log.Message,
                    LogLevel = log.LogLevel,
                    Timestamp = log.Timestamp
                })
                .ToList();

            var traces = _context.Traces
                .Where(trace => trace.RequestId == requestId)
                .OrderByDescending(log => log.Timestamp)
                .Select(trace => new TraceViewModel
                {
                    Id = trace.Id,
                    Message = trace.Message,
                    Timestamp = trace.Timestamp
                })
                .ToList();

            var model = new LogDetailViewModel
            {
                Logs = logs,
                Traces = traces
            };

            return View(model);
        }
    }

}
