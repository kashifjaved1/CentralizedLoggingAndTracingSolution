using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class ActivityDbContext : DbContext
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options) { }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Trace> Traces { get; set; }
    }
}
