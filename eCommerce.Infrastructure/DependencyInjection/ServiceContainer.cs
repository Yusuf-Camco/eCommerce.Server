using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Authentications;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Middleware;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Authentication;
using eCommerce.Infrastructure.Repositories.Cart;
using eCommerce.Infrastructure.Repositories.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            services.AddDefaultIdentity<AppUser>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredUniqueChars = 1;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JWT:Issuer"],
                        ValidAudience = config["JWT:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                    };

                    //opt.Audience = config["JWT:Audience"];
                    //opt.Authority = config["JWT:Authority"];
                    //opt.RequireHttpsMetadata = false;
                });
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IRoleManagement, RoleManagement>();
            services.AddScoped<ITokenManagement, TokenManagement>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepo>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }

}