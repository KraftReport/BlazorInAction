using blazortrailsapi.Persistence.Entities;
using Microsoft.EntityFrameworkCore; 

namespace blazortrailsapi.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Trail> Trails => Set<Trail>();
        public DbSet<RouteInstruction> Routes => Set<RouteInstruction>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrailConfig());
            modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
        }

    }
}
