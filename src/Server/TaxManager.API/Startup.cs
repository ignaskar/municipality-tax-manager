using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TaxManager.API.Errors;
using TaxManager.API.Extensions;
using TaxManager.API.Middleware;
using TaxManager.Core.Interfaces;
using TaxManager.Infra.Data;

namespace TaxManager.API
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
            services.AddControllers();

            services.RegisterApplicationServices();
            services.RegisterSwaggerServices();
            
            services.AddDbContext<TaxManagerContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("SQLDB"));
            });
            
            services.AddCors(o =>
            {
                o.AddPolicy("allowBlazor", builder =>
                {
                    builder.WithOrigins("https://localhost:5003", "https://localhost:5002")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/api/v1/errors/{0}");

            app.UseSwaggerServices();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("allowBlazor");
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}