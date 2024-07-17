using Core.Data;
using Microsoft.AspNetCore.Mvc;
using UI.Data.ViewModels;

namespace UI.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly ActivityDbContext _context;

        public ResponsesController(ActivityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var responses = _context.Responses
                .Select(r => new ResponseViewModel
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    StatusCode = r.StatusCode,
                    Headers = r.Headers,
                    Body = r.Body,
                    Timestamp = r.Timestamp
                })
                .ToList();
            return View(responses);
        }

        public IActionResult Details(int id)
        {
            var response = _context.Responses
                .Where(r => r.Id == id)
                .Select(r => new ResponseViewModel
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    StatusCode = r.StatusCode,
                    Headers = r.Headers,
                    Body = r.Body,
                    Timestamp = r.Timestamp
                })
                .FirstOrDefault();

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }
    }

}
