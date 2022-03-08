using Microsoft.AspNetCore.Mvc.Filters;

namespace DesafioDev.Core.Interfaces
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
