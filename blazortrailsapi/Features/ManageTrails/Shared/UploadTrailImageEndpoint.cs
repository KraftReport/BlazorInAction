using Ardalis.ApiEndpoints;
using Azure.Core;
using blazortrailsapi.Persistence;
using blazortrailsshared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Data.Entity;

namespace blazortrailsapi.Features.ManageTrails.Shared
{
    public class UploadTrailImageEndpoint : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<string>
    {
        private readonly AppDbContext _appDbContext;

        public UploadTrailImageEndpoint(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost(UploadImageRequest.RouteTemplate)]
        public override async Task<ActionResult<string>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
        {
            try
            {
                var trail =  _appDbContext.Trails.FirstOrDefault(trail => trail.Id == trailId);

                if (trail is null) { return BadRequest("Trail is not exist"); }

                var file = Request.Form.Files[0];

                if (file is null) { return BadRequest("No image found"); }

                var fileName = $"{Guid.NewGuid()}.jpg";

                var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

                var resizeOptions = new ResizeOptions()
                {
                    Mode = ResizeMode.Pad,
                    Size = new Size(640, 426)
                };

                using (var image = Image.Load(file.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(resizeOptions));
                    await image.SaveAsJpegAsync(saveLocation, cancellationToken);
                }

                if (string.IsNullOrWhiteSpace(trail.Image))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));
                }

                trail.Image = fileName;
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return Ok(fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
