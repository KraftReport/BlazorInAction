using blazortrailsapi.Persistence.Entities;
using Microsoft.EntityFrameworkCore; 

namespace blazortrailsapi.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Trail> Trails => Set<Trail>();
        public DbSet<Waypoint> Waypoints => Set<Waypoint>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrailConfig());
            modelBuilder.ApplyConfiguration(new WaypointConfig());
        }

    }
}
