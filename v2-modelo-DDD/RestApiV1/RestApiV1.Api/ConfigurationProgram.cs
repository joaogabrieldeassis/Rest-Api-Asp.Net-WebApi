using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestApi.Infra.CrossCutting.Ioc;
using RestApiV1.Infra.Context;

namespace RestApiV1.Api
{
    public static class ConfigurationProgram
    {
        public static void ConfigurarStringDeConexecao(ref WebApplicationBuilder builder)
        {
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MeuDbContext>(c =>
            {
                c.UseSqlServer(connection);
            });
        }
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            ConfigIoc.ConfigurarInjecaoDeDependencia(services);
        }
    }
}
