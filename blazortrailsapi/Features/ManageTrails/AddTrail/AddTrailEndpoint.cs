using Ardalis.ApiEndpoints;
using blazortrailsapi.Persistence;
using blazortrailsapi.Persistence.Entities;
using blazortrailsshared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Data.Entity;

namespace blazortrailsapi.Features.ManageTrails.AddTrail
{
    public class AddTrailEndpoint : EndpointBaseAsync
        .WithRequest<AddTrailRequest>
        .WithActionResult<AddTrailRequest.Response>
    {
        private readonly AppDbContext _appDbContext;

        public AddTrailEndpoint(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost(AddTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<AddTrailRequest.Response>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = new Trail
            {
                Name = request.TrailDto.Name,
                Description = request.TrailDto.Description,
                Location = request.TrailDto.Location,
                Length = request.TrailDto.Length,
                TimeInMinutes = request.TrailDto.TimeInMinutes
            };

            await _appDbContext.Trails.AddAsync(trail);

            var waypoints = request.TrailDto.Waypoints
                .Select(x => new Waypoint
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Trail = trail
                }); 

            await _appDbContext.Waypoints.AddRangeAsync(waypoints);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Ok(trail.Id);
        }
    }


}
