using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers.Base;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class RequestsController : BaseController
    {
        private readonly ActivityDbContext _context;

        public RequestsController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var requests = _context
                .Requests
                .OrderByDescending(r => r.Timestamp)
                .Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    ServiceName = r.ServiceName,
                    Url = r.Url,
                    Method = r.Method,
                    Headers = r.Headers,
                    Body = r.Body,
                    Timestamp = r.Timestamp
                })
                .ToList();
            return View(requests);
        }

        public IActionResult Details(int id)
        {
            var request = _context.Requests
                .Where(r => r.Id == id)
                .Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    ServiceName = r.ServiceName,
                    Url = r.Url,
                    Method = r.Method,
                    Headers = r.Headers,
                    Body = r.Body,
                    Timestamp = r.Timestamp
                })
                .FirstOrDefault();

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }
    }
}
