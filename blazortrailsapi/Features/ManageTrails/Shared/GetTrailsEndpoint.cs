using Ardalis.ApiEndpoints;
using blazortrailsapi.Persistence;
using blazortrailsshared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace blazortrailsapi.Features.ManageTrails.Shared
{
    public class GetTrailsEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetTrailsRequest.Response>
    {
        private readonly AppDbContext _appDbContext;

        public GetTrailsEndpoint(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet(GetTrailsRequest.RouteTemplate)]
        public override Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var trails = _appDbContext.Trails.Select(x => new GetTrailsRequest.Trail(
                x.Id, x.Name, x.Description, x.Length, x.Location, x.TimeInMinutes, x.Image));

            return Task.FromResult<ActionResult<GetTrailsRequest.Response>>(
                Ok(new GetTrailsRequest.Response(trails)));
        }
    }
}
