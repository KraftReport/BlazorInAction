using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazortrailsshared.Features.ManageTrails.Shared
{
    public record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response>
    {
        public const string RouteTemplate = "api/trails/{trailId}";
        public record Response(Trail TrailDto);
        public record Trail(int Id,string Name,string Description,string Location,int Length,int TimeInMinutes,string? Image,IEnumerable<RouteInstruction> RouteInstructions);
        public record RouteInstruction(int Stage,string Description,int Id);
    }
}
