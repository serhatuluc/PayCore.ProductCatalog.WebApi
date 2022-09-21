using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnionArcExample.Application;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.IOC;
using PayCore.ProductCatalog.Infrastructure;
using PayCore.ProductCatalog.Infrastructure.IOC;
using PayCore.ProductCatalog.Persistence.DependencyContainers;


namespace PayCore.ProductCatalog.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependecies of layers
            services.AddApplicationServices(Configuration);
            services.AddInfrastructureServices(Configuration);
            services.AddPersistenceServices(Configuration);
            services.AddHangfireServices(Configuration);

 
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddCustomizeSwagger();
            services.AddControllers();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayCore.ProductCatalog.WebAPI v1"));
            }
            // Exception middleware
            app.UseMiddleware<ExceptionMiddleware>();
            

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            //Hanfire dashboard
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
