using Core.UOW;
using Microsoft.AspNetCore.Http;

namespace Core.Middlewares
{
    public class AutoSaveChangesMiddleware : IMiddleware
    {
        private readonly IUnitOfWork _uow;

        public AutoSaveChangesMiddleware(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            await _uow.SaveAsync();
        }
    }
}
