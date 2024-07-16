using Core.Data;
using Core.Filters;
using Core.Middlewares;
using Core.Services.Implementation;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
});

services.AddControllersWithViews(options =>
{
    options.Filters.Add<ActionFilter>();
});
services.AddTransient<ExceptionMiddleware>();

services.AddDbContext<ActivityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
services.AddTransient<ILoggerService>(provider =>
    new LoggerService(provider.GetRequiredService<ActivityDbContext>(), provider.GetRequiredService<IHttpContextAccessor>(), "MVC"));
services.AddHttpContextAccessor();

services.AddOpenTelemetry().WithTracing(bldr => bldr
.AddAspNetCoreInstrumentation()
.AddConsoleExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
