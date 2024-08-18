using Core.Data;
using Core.Filters;
using Core.Middlewares;
using Core.Services.Implementation;
using Core.Services.Interfaces;
using Core.UOW;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers(options =>
{
    options.Filters.Add<ActionFilter>();
});

services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddDbContext<ActivityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
services.AddTransient<ILoggerService>(provider =>
new LoggerService(provider.GetRequiredService<IHttpContextAccessor>(), provider.GetRequiredService<IUnitOfWork>()));
services.AddTransient<IMetricsService>(provider =>
    new MetricsService(provider.GetRequiredService<IHttpContextAccessor>(), provider.GetRequiredService<IUnitOfWork>()));
services.AddTransient<IRequestResponseService>(provider =>
    new RequestResponseService(provider.GetRequiredService<IHttpContextAccessor>(), provider.GetRequiredService<IUnitOfWork>()));
services.AddHttpContextAccessor();
services.AddTransient<ExceptionMiddleware>();
services.AddTransient<RequestLoggingMiddleware>();
services.AddTransient<ResponseLoggingMiddleware>();
services.AddTransient<MetricsLoggingMiddleware>();

services.AddOpenTelemetry().WithTracing(bldr => bldr
.AddAspNetCoreInstrumentation()
.AddConsoleExporter());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7267")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<MetricsLoggingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ResponseLoggingMiddleware>();

app.Run();