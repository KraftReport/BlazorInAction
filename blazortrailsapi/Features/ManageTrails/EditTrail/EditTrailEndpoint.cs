using Ardalis.ApiEndpoints;
using blazortrailsapi.Persistence;
using blazortrailsapi.Persistence.Entities;
using blazortrailsshared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;

namespace blazortrailsapi.Features.ManageTrails.EditTrail
{
    public class EditTrailEndpoint : EndpointBaseAsync
        .WithRequest<EditTrailRequest>
        .WithActionResult<EditTrailRequest.Response>
    {

        private readonly AppDbContext _appDbContext;

        public EditTrailEndpoint(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPut(EditTrailRequest.RouteTemplate)]
        public override async Task<ActionResult<EditTrailRequest.Response>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
        {
            var trail = _appDbContext.Trails.FirstOrDefault(x => x.Id == request.TrailDto.Id);

            var waypoints = _appDbContext.Waypoints.Where(x => x.TrailId == request.TrailDto.Id).ToList();

            trail.Waypoints = waypoints;

            if (trail is null)
            {
                return BadRequest("trail is not found");
            }

            trail.Name = request.TrailDto.Name;
            trail.Description = request.TrailDto.Description;
            trail.Location = request.TrailDto.Location;
            trail.Length = request.TrailDto.Length;
            trail.TimeInMinutes = request.TrailDto.TimeInMinutes;
            trail.Waypoints.Clear();
            trail.Waypoints = request.TrailDto.Waypoints.
                Select(x => new Waypoint
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Trail = trail
                }).ToList(); 

            if(request.TrailDto.ImageAction == blazortrailsshared.Features.ManageTrails.Shared.ImageAction.DELETE)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(),"Images",trail.Image));
                trail.Image = null;
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Ok(true);
        }
    }
}
