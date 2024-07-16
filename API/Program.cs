using Core.Data;
using Core.Filters;
using Core.Middlewares;
using Core.Services.Implementation;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers(options =>
{
    options.Filters.Add<ActionFilter>();
});

services.AddDbContext<ActivityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
services.AddTransient<ILoggerService>(provider =>
        new LoggerService(provider.GetRequiredService<ActivityDbContext>(), provider.GetRequiredService<IHttpContextAccessor>(), "API"));
services.AddHttpContextAccessor();
services.AddTransient<ExceptionMiddleware>();

services.AddOpenTelemetry().WithTracing(bldr => bldr
.AddAspNetCoreInstrumentation()
.AddConsoleExporter());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();