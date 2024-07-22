using Core.Data;
using Core.Data.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class MetricsLoggingMiddleware : IMiddleware
    {
        private readonly IMetricsService _metricsService;

        public MetricsLoggingMiddleware(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await next(context);
            }
            finally
            {
                stopwatch.Stop();

                _metricsService.LogMetric("ResponseTime", stopwatch.ElapsedMilliseconds, null);
            }
        }
    }

}
