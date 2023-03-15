using System.Runtime.CompilerServices;

namespace RestApi.Configuration
{
    public static class LoggerConfig

    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
        {
            services.AddElmahIo(z =>
            {
                z.ApiKey = "b7f5d2f7bc79455ab626fd643ed8c1fd";
                z.LogId = new Guid("1850d29a-1aa8-4282-8c7d-54c2992d8261");
            });
            return services;
        }
        public static WebApplication UseLoggingConfiguration(this WebApplication builder)
        {
            
            builder.UseElmahIo();
            return builder;
        }
    }
}
