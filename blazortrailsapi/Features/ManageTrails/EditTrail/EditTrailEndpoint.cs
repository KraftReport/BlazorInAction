using Ardalis.ApiEndpoints;
using blazortrailsapi.Persistence;
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

            var routes = _appDbContext.Routes.Where(x => x.TrailId == request.TrailDto.Id).ToList();

            trail.Route = routes;

            if (trail is null)
            {
                return BadRequest("trail is not found");
            }

            trail.Name = request.TrailDto.Name;
            trail.Description = request.TrailDto.Description;
            trail.Location = request.TrailDto.Location;
            trail.Length = request.TrailDto.Length;
            trail.TimeInMinutes = request.TrailDto.TimeInMinutes;
            trail.Route.Clear();
            trail.Route = request.TrailDto.RouteInstructions.
                Select(x => new Persistence.Entities.RouteInstruction
                {
                    Stage = x.State,
                    Description = x.Description,
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
