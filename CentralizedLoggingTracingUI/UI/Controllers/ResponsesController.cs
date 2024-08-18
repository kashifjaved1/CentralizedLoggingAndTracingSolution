using Core.Data;
using Core.Data.Entities;
using Core.UOW;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class ResponsesController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public ResponsesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var responses = _uow.Repository<Response>()
                .GetAllOrdered(res => res.TenantId == TenantId, res => res.Timestamp, true)
                .Select(response => new ResponseViewModel
                {
                    Id = response.Id,
                    RequestId = response.RequestId,
                    StatusCode = response.StatusCode,
                    Headers = response.Headers,
                    Body = response.Body,
                    Timestamp = response.Timestamp
                })
                .ToList();
            return View(responses);
        }

        public IActionResult Details(int id)
        {
            var response = _uow.Repository<Response>()
                .GetById(res => res.Id == id && res.TenantId == TenantId);
            var responseViewModel = new ResponseViewModel
            {
                Id = response.Id,
                RequestId = response.RequestId,
                StatusCode = response.StatusCode,
                Headers = response.Headers,
                Body = response.Body,
                Timestamp = response.Timestamp
            };

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }
    }

}
