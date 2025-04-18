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

            var routeInstructions = request.TrailDto.RouteInstructions
                .Select(x => new RouteInstruction
                {
                    Stage = x.State,
                    Description = x.Description,
                    Trail = trail
                });

            await _appDbContext.Routes.AddRangeAsync(routeInstructions);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Ok(trail.Id);
        }
    }


}
