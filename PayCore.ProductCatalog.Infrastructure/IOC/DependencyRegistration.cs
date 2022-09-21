using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.Interfaces.Mail;
using PayCore.ProductCatalog.Domain.Mail;
using PayCore.ProductCatalog.Infrastructure.MailManager;

namespace PayCore.ProductCatalog.Infrastructure.IOC
{
    public static class DependencyRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<ILoggerManager,LoggerManager>();
            services.AddSingleton<IEmailService,EmailService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        }
    }
}
