using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
using DesafioDev.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDev.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<DesafioDevContext>();

            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}
