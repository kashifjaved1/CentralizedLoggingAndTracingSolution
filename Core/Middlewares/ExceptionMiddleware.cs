using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using OpenTelemetry;
using System.Diagnostics;

namespace Core.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILoggerService _loggerService;

        public ExceptionMiddleware(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Exception: {ex.Message}");
                using var activity = new ActivitySource("Exception Activity").StartActivity("Exception");
                activity?.SetTag("Exception", ex.Message);
                throw;
            }
        }
    }

}
