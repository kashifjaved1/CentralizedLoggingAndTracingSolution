using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ILoggerService _loggerService;

        public BaseController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public BaseController()
        {
            
        }
    }
}
