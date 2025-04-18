using blazortrailsshared.Features.ManageTrails.Shared;
using MediatR;

namespace blazortrailsclient.Features.ManageTrails.Shared
{
    public class UploadTrailImageHandler : IRequestHandler<UploadImageRequest, UploadImageRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public UploadTrailImageHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadImageRequest.Response> Handle(UploadImageRequest request, CancellationToken cancellationToken)
        {
            var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

            using(var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(fileContent), "image", request.File.Name);

                var response = await _httpClient.PostAsync(UploadImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()), content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var fileName = await response.Content.ReadAsStringAsync(cancellationToken);
                    return new UploadImageRequest.Response(fileName);
                }

                return new UploadImageRequest.Response(string.Empty);
            }

        }
    }
}
