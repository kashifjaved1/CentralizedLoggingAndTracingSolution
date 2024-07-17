using Core.Data;
using Core.Data.Entities;
using Core.Services.Implementation;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly IRequestResponseService _requestResponseService;

        public RequestLoggingMiddleware(IRequestResponseService requestResponseService)
        {
            _requestResponseService = requestResponseService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                string requestBody = await ReadRequestBodyAsync(context.Request);
                
                var request = new Request
                {
                    Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                    Method = context.Request.Method,
                    Headers = SerializeHeaders(context.Request.Headers),
                    Body = requestBody
                };

                _requestResponseService.LogRequest(request);

                await next(context);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }

        private string SerializeHeaders(IHeaderDictionary headers)
        {
            return string.Join("; ", headers.Select(h => $"{h.Key}: {h.Value}"));
        }
    }

}
