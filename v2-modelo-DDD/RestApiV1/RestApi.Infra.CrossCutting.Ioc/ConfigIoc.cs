using Microsoft.Extensions.DependencyInjection;
using RestApiV1.Domain.Interfaces.Notification;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Domain.Service;
using RestApiV1.Domain.Service.Notifications;
using RestApiV1.Infra.Context;
using RestApiV1.Infra.Repositorios;

namespace RestApi.Infra.CrossCutting.Ioc
{
    public static class ConfigIoc
    {
        public static void ConfigurarInjecaoDeDependencia(IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepositório>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

        }
    }
}
