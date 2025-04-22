using Ardalis.ApiEndpoints;
using blazortrailsapi.Persistence;
using blazortrailsapi.Persistence.Entities;
using blazortrailsshared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace blazortrailsapi.Features.ManageTrails.Shared
{
    public class GetTrailEndpoint : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<GetTrailRequest.Response>
    {

        private readonly AppDbContext _appDbContext;

        public GetTrailEndpoint(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet(GetTrailRequest.RouteTemplate)]
        public override Task<ActionResult<GetTrailRequest.Response>> HandleAsync([FromRoute]int trailId, CancellationToken cancellationToken = default)
        {
            var trail = _appDbContext.Trails.Include(t=>t.Waypoints).FirstOrDefault(x => x.Id == trailId);

            var waypoints = _appDbContext.Waypoints.Where(r => r.TrailId == trail.Id).ToList();

            trail.Waypoints = waypoints;

            if(trail is null)
            {
                return Task.FromResult<ActionResult<GetTrailRequest.Response>>(NotFound());
            }

            var response = new GetTrailRequest.Response(
                new GetTrailRequest.Trail(
                    trail.Id,
                    trail.Name,
                    trail.Description,
                    trail.Location,
                    trail.Length,
                    trail.TimeInMinutes,
                    trail.Image,
                    trail.Waypoints.Select(x => new GetTrailRequest.Waypoint(x.Latitude,x.Longitude))));

                return Task.FromResult<ActionResult<GetTrailRequest.Response>>(Ok(response));
        }
    }
}
