using API.Controllers.Base;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WelcomeController : BaseController
    {
        private readonly ILoggerService log;

        public WelcomeController(ILoggerService _log) : base(_log)
        {
            log = _log;
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            log.LogInformation("API: HelloController.Get() called");
            return Ok($"Welcome {username}!");
        }
    }
}
