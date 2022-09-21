using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PayCore.ProductCatalog.Application.IOC
{
    public static class DependencyRegistration
    {
        public static void AddHangfireServices(this IServiceCollection services, IConfiguration Configuration)
        {
            //Hangfire registration is done here
            services.AddHangfire(configuration => configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSqlConnection"), new PostgreSqlStorageOptions
             {
                 TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                 InvisibilityTimeout = TimeSpan.FromMinutes(5),
                 QueuePollInterval = TimeSpan.FromMinutes(5),
             }));

            services.AddHangfireServer();

        }
    }
}