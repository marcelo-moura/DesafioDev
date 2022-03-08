using DesafioDev.Application.Enricher;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services;
using DesafioDev.Core.Hypermedia.Filters;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDev.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            #region Register Context
            services.AddScoped<DesafioDevContext>();
            #endregion

            #region Register Servies
            services.AddScoped<IProdutoService, ProdutoService>();
            #endregion

            #region Register Repositories
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion

            services.AddScoped<INotificador, Notificador>();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new ProdutoEnricher());

            services.AddSingleton(filterOptions);

            return services;
        }
    }
}
