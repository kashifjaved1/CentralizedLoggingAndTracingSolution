using Core.Data;
using Core.Data.Entities;
using Core.UOW;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class LogsController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public LogsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _uow.Repository<Log>().GetAllOrderedAsync(null, log => log.Timestamp, orderByDescending: true);
            var logViewModel = logs
            .Select(log => new LogViewModel
            {
                Id = log.Id,
                RequestId = log.RequestId,
                Message = log.Message,
                LogLevel = log.LogLevel,
                Timestamp = log.Timestamp
            }).ToList();

            return View(logViewModel);
        }

        public async Task<IActionResult> Details(Guid requestId)
        {
            var logs = await _uow.Repository<Log>().GetAllOrderedAsync(null, log => log.Timestamp, orderByDescending: true);
            var logViewModel = logs
                .OrderByDescending(log => log.Timestamp)
                .Select(log => new LogViewModel
                {
                    Id = log.Id,
                    Message = log.Message,
                    LogLevel = log.LogLevel,
                    Timestamp = log.Timestamp
                })
                .ToList();


            var traces = await _uow.Repository<Trace>().GetAllOrderedAsync(trace => trace.RequestId == requestId, log => log.Timestamp, orderByDescending: true);
            var traceViewModel = traces
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
                Logs = logViewModel,
                Traces = traceViewModel
            };

            return View(model);
        }
    }

}
