using Microsoft.Extensions.DependencyInjection;
using RestApiV1.Domain.Interfaces.Notification;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Infra.Repositorios;


namespace RestApiV1.Infra.CrossCutting.Ioc
{
    public static class ConfigIoc
    {
        static void ConfigurarInjecaoDeDependencia(IServiceCollection services)
        {
            services.AddScoped<IFornecedorRepository, FornecedorRepositório>();
            
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            
            //services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, >();
            services.AddScoped<IProdutoService, ProdutoService>();
          
            return builder;
        }
    }
}
