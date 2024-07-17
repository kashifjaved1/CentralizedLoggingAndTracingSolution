using Core.Data.Entities;

namespace Core.Services.Interfaces
{
    public interface IRequestResponseService
    {
        void LogRequest(Request request);
        void LogResponse(Response response);
    }
}