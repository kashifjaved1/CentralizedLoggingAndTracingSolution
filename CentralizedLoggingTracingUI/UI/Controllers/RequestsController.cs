using Core.Data;
using Core.Data.Entities;
using Core.UOW;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class RequestsController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public RequestsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var requests = _uow.Repository<Request>()
                .GetAllOrdered(req => req.TenantId == TenantId, req => req.Timestamp, true)
                .Select(request => new RequestViewModel
                {
                    Id = request.Id,
                    RequestId = request.RequestId,
                    ServiceName = request.ServiceName,
                    Url = request.Url,
                    Method = request.Method,
                    Headers = request.Headers,
                    Body = request.Body,
                    Timestamp = request.Timestamp
                })
                .ToList();
            return View(requests);
        }

        public IActionResult Details(int id)
        {
            var request = _uow.Repository<Request>()
                .GetById(req => req.Id == id && req.TenantId == TenantId);
            var requestViewModel = new RequestViewModel
            {
                Id = request.Id,
                RequestId = request.RequestId,
                ServiceName = request.ServiceName,
                Url = request.Url,
                Method = request.Method,
                Headers = request.Headers,
                Body = request.Body,
                Timestamp = request.Timestamp
            };

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }
    }
}
