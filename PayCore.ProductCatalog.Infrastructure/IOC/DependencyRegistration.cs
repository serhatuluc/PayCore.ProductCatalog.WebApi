using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayCore.ProductCatalog.Application.Interfaces.Log;


namespace PayCore.ProductCatalog.Infrastructure.IOC
{
    public static class DependencyRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<ILoggerManager,LoggerManager>();
        }
    }
}
