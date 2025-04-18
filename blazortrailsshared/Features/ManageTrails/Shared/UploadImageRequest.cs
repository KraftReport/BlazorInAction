using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace blazortrailsshared.Features.ManageTrails.Shared
{
    public record UploadImageRequest(int TrailId,IBrowserFile File) : IRequest<UploadImageRequest.Response>
    {
        public const string RouteTemplate = "/api/trails/{trailId}/images";

        public record Response(string imageName);
    }
}
