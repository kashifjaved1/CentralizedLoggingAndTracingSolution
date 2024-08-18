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

        public IActionResult Index()
        {
            var logs = _uow.Repository<Log>()
                .GetAllOrdered(log => log.TenantId == TenantId, log => log.Timestamp, orderByDescending: true)
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
            var log = _uow.Repository<Log>()
                .GetById(log => log.RequestId == requestId && log.TenantId == TenantId);

            var logViewModel = new LogViewModel
            {
                Id = log.Id,
                Message = log.Message,
                LogLevel = log.LogLevel,
                Timestamp = log.Timestamp
            };


            var traces = _uow.Repository<Trace>()
                .GetAllOrdered(filter: trace => trace.RequestId == requestId && trace.TenantId == TenantId, orderByKeySelector: log => log.Timestamp, orderByDescending: true)
                .Select(trace => new TraceViewModel
                {
                    Id = trace.Id,
                    Message = trace.Message,
                    Timestamp = trace.Timestamp
                })
                .ToList();

            var model = new LogDetailViewModel
            {
                Log = logViewModel,
                Traces = traces
            };

            return View(model);
        }
    }

}
