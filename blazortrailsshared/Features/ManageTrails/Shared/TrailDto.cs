using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazortrailsshared.Features.ManageTrails.Shared
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public string? Image { get; set; }
        public ImageAction ImageAction { get; set; }
        public List<WaypointDto> Waypoints { get; set; } = new List<WaypointDto>();
        public record WaypointDto(decimal Latitude, decimal Longitude);
    }

    public enum ImageAction
    {
        ADD,DELETE,NONE
    }

    public class TrailValidattor : AbstractValidator<TrailDto>
    {
        public TrailValidattor()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length");
            RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter a time");
            RuleFor(x => x.Waypoints).NotEmpty().WithMessage("Please enter waypoints");  
        }
    }

  
   

 
}
