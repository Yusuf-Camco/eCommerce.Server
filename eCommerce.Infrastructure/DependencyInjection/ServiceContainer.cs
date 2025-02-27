using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
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
        private static string connectionString = "DefaultConnection";

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "DefaultConnection";
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(config.GetConnectionString(connectionString),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure();
                    }),
                    ServiceLifetime.Scoped);
           

            services.AddScoped(typeof(IGeneric<Product>), typeof(Generic<Product>)); 
            services.AddScoped(typeof(IGeneric<Category>), typeof(Generic<Category>));

            return services; // Added missing return statement
        }
    }

}