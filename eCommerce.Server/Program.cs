using eCommerce.Application.DependencyInjection;
using eCommerce.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
// Add services to the container.


builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
builder.Host.UseSerilog();
Log.Logger.Information("Application Starting........");



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:44345", "https://localhost:44345")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

try
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseCors();
    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseInfrastructureService();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Logger.Information("Application Started........");
    app.Run();
}
catch (Exception ex)
{

    Log.Logger.Error(ex, "Application failed to start.....");
}
finally
{
    Log.CloseAndFlush();
}
