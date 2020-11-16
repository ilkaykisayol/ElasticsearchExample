using ProductService.Core.Models;

namespace ProductService.Core.Services.Interfaces
{
    public interface IElasticsearchService
    {
        void InsertActionLog(ActionLogModel actionLogModel);
        void InsertErrorLog(ErrorLogModel errorLogModel);
    }
}
