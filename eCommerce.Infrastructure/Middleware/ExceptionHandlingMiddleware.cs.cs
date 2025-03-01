using eCommerce.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
    {

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await requestDelegate(context);
            }
            catch (DbUpdateException ex)
            {
                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                context.Response.ContentType = "application/json";
                if (ex.InnerException is SqlException innerException)
                {
                    logger.LogError(innerException, "Sql Exception");

                    switch (innerException.Number)
                    {
                        case 547:
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Foreign Key Constraint Violation");
                            break;
                        case 2627:
                            context.Response.StatusCode = 409;
                            await context.Response.WriteAsync("This record already exists");
                            break;
                        case 515:
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Some fields are required");
                            break;
                        case 4060:
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("Invalid database");
                            break;
                        default:
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("An error occurred while processing your request");
                            break;
                    }
                }
                else
                {
                    logger.LogError(ex, "Related EfCore Exception");
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An error occurred while processing your request");
                }
            }
            catch (Exception ex)
            {
                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                logger.LogError(ex, "An error occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred: " + ex.Message);
            }

        }
    }
}
