using Core.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = Guid.NewGuid(), Name = "Tenant 1", Identifier = "tenant1" },
                new Tenant { Id = Guid.NewGuid(), Name = "Tenant 2", Identifier = "tenant2" },
                new Tenant { Id = Guid.NewGuid(), Name = "Tenant 3", Identifier = "tenant3" },
                new Tenant { Id = Guid.NewGuid(), Name = "Tenant 4", Identifier = "tenant4" }
            );

            var tenantId = _httpContextAccessor?.HttpContext?.Items["TenantId"]?.ToString();

            modelBuilder.Entity<Log>().HasQueryFilter(log => log.TenantId == tenantId);
            modelBuilder.Entity<Metric>().HasQueryFilter(metric => metric.TenantId == tenantId);
            modelBuilder.Entity<Trace>().HasQueryFilter(trace => trace.TenantId == tenantId);
            modelBuilder.Entity<Request>().HasQueryFilter(request => request.TenantId == tenantId);
            modelBuilder.Entity<Response>().HasQueryFilter(response => response.TenantId == tenantId);
        }

    }
}
