using Core.Data.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class ResponseLoggingMiddleware : IMiddleware
    {
        private readonly IRequestResponseService _requestResponseService;

        public ResponseLoggingMiddleware(IRequestResponseService requestResponseService)
        {
            _requestResponseService = requestResponseService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var originalBodyStream = context.Response.Body;

            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            var responseLog = new Response
            {
                StatusCode = context.Response.StatusCode,
                Headers = SerializeHeaders(context.Response.Headers),
                Body = responseBody,
                Timestamp = DateTime.UtcNow
            };

            _requestResponseService.LogResponse(responseLog);

            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        private string SerializeHeaders(IHeaderDictionary headers)
        {
            return string.Join("; ", headers.Select(h => $"{h.Key}: {h.Value}"));
        }
    }

}
