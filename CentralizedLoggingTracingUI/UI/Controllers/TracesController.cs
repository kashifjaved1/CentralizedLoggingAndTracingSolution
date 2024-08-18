using Core.Data;
using Core.Data.Entities;
using Core.UOW;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class TracesController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public TracesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var traces = _uow.Repository<Trace>()
                .GetAllOrdered(trace => trace.TenantId == TenantId, trace => trace.Timestamp, true)
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
