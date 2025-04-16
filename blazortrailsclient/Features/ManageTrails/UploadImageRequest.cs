using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace blazortrailsclient.Features.ManageTrails
{
    public record UploadImageRequest(int TrailId,IBrowserFile File) : IRequest<UploadImageRequest.Response>
    {
        public const string RouteTemplate = "/api/trails/{trailId}/images";

        public record Response(string imageName);
    }
}
