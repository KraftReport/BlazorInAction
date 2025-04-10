using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blazortrailsapi.Persistence.Entities
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Image { get; set; }
        public string Location { get; set; } = default!;
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public ICollection<RouteInstruction> Route { get; set; } = default!;
    }

    public class TrailConfig : IEntityTypeConfiguration<Trail>
    {
        public void Configure(EntityTypeBuilder<Trail> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.TimeInMinutes).IsRequired();
            builder.Property(x => x.Length).IsRequired(); 
        }
    }

    public class RouteInstruction
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; } = default!;
        public Trail Trail { get; set; } = default!;
    }

    public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
    {
        public void Configure(EntityTypeBuilder<RouteInstruction> builder)
        {
            builder.Property(x => x.TrailId).IsRequired();
            builder.Property(x => x.Stage).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
