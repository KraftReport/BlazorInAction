using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazortrailsshared.Features.ManageTrails.Shared
{
    public class GetTrailsRequest : IRequest<GetTrailsRequest.Response>
    {
        public const string RouteTemplate = "/api/trails";

        public record Response(IEnumerable<Trail> Trails);

        public record Trail(int Id,string Name,string Description,int Length,string Location,int TimeInMinutes,string? Image,List<Waypoint> Waypoints);

        public record Waypoint(decimal Latitude, decimal Longitude);
    }
}
