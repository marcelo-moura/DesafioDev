using DesafioDev.Application.Enricher;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services;
using DesafioDev.Core.Hypermedia.Filters;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.Integration.MercadoPago;
using DesafioDev.Infra.Integration.MercadoPago.Interfaces;
using DesafioDev.Infra.Integration.RabbitMQ;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace DesafioDev.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            #region Register Context
            services.AddScoped<DesafioDevContext>();
            #endregion

            #region Register Services
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPedidoService, PedidoService>();
            #endregion

            #region Register Repositories
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();
            #endregion

            services.AddScoped<INotificador, Notificador>();

            #region Policies
            var retryPolicy = HttpPolicyExtensions
                             .HandleTransientHttpError()
                             .WaitAndRetryAsync(int.Parse(configuration["NumeroTentativas"]), retryAttempt => TimeSpan.FromSeconds(retryAttempt));
            #endregion

            #region Integrations Configuration            
            services.AddScoped<IMercadoPagoGateway, MercadoPagoGateway>();
            services.AddScoped<IRabbitMQMessageSender, RabbitMQMessageSender>();
            services.AddScoped<IRabbitMQMessageConsumer, RabbitMQMessageConsumer>();
            #endregion            

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new ProdutoEnricher());

            services.AddSingleton(filterOptions);

            return services;
        }
    }
}
