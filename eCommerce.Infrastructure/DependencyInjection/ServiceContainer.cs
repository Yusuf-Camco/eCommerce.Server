using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Middleware;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "DefaultConnection";
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(config.GetConnectionString(connectionString),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure();
                    }).UseExceptionProcessor(),
                    ServiceLifetime.Scoped);


            services.AddScoped(typeof(IGeneric<Product>), typeof(Generic<Product>));
            services.AddScoped(typeof(IGeneric<Category>), typeof(Generic<Category>));
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));      

            return services;
        }

        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }

}