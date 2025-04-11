using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazortrailsshared.Features.ManageTrails
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public List<RouteInstruction> RouteInstructions { get; set; } = new List<RouteInstruction>();

        public class RouteInstruction
        {
            public int State { get; set; }
            public string Description { get; set; } = string.Empty;
        }
    }
}
