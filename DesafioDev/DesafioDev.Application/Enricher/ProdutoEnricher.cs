using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Core.Hypermedia;
using DesafioDev.Core.Hypermedia.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DesafioDev.Application.Enricher
{
    public class ProdutoEnricher : ContentResponseEnricher<ProdutoViewModelSaida>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(ProdutoViewModelSaida content, IUrlHelper urlHelper)
        {
            var path = "api/v1/produto";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return Task.CompletedTask;
        }

        private string GetLink(Guid? id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };                               
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").Replace("?version=1", "").ToString();
            }
        }
    }
}
