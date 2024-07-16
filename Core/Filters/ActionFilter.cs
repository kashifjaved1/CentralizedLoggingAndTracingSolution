using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly ILoggerService _loggerService;

        public ActionFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.DisplayName;
            _loggerService.Trace($"{controller} - {action} start");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.DisplayName;
            _loggerService.Trace($"{controller} - {action} end");
        }
    }
}
