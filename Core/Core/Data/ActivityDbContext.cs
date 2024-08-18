using Core.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Core.Data
{
    public class ActivityDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActivityDbContext(DbContextOptions<ActivityDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Trace> Traces { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var defaultTenant = new Tenant { Id = 1, Name = "Default", Identifier = "default" };
            var tenantList = new List<Tenant> { defaultTenant };

            for (int i = 2; i <= 4; i++)
            {
                tenantList.Add(new Tenant { Id = i, Name = $"Tenant {i - 1}", Identifier = $"tenant{i - 1}" });
            }

            modelBuilder.Entity<Tenant>().HasData(
                tenantList
            );

            var tenantId = _httpContextAccessor?.HttpContext?.Items["TenantId"]?.ToString() ?? defaultTenant.Id.ToString();

            // Global Query Filters won't work for singleDb-multi-tenant architecture. Why? here's the answer:
            // - Global query filters are configured during the OnModelCreating method of DbContext, which is executed when the model is built.
            //   This happens once when the DbContext is first created or when the application starts, not dynamically during query execution.
            // - If you change the tenant context after the DbContext has been instantiated, the global query filters won’t automatically reflect
            //   this change because they were set up when the model was created.

            //modelBuilder.Entity<Log>().HasQueryFilter(log => log.TenantId == tenantId);
            //modelBuilder.Entity<Metric>().HasQueryFilter(metric => metric.TenantId == tenantId);
            //modelBuilder.Entity<Trace>().HasQueryFilter(trace => trace.TenantId == tenantId);
            //modelBuilder.Entity<Request>().HasQueryFilter(request => request.TenantId == tenantId);
            //modelBuilder.Entity<Response>().HasQueryFilter(response => response.TenantId == tenantId);
        }

    }
}
