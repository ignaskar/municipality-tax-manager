using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TaxManager.API.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection RegisterSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TaxManager.API", Version = "v1"});
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxManager.API v1"));

            return app;
        }
    }
}