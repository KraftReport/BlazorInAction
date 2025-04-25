using blazortrailsshared.Features.ManageTrails.Shared;
using MediatR;
using System.Net.Http.Json;

namespace blazortrailsclient.Features.ManageTrails.Shared
{
    public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
    {

        private readonly HttpClient _httpClient;

        public GetTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"tid -> {request.TrailId}");
                var response = await _httpClient.GetFromJsonAsync<GetTrailRequest.Response>(
                    $"{GetTrailRequest.RouteTemplate.Replace("{trailId}",
                    request.TrailId.ToString())}",
                    cancellationToken: cancellationToken);

                return response ?? throw new Exception("Failed to retrieve trail data.");

            }
            catch (Exception ex)
            {
                return default;
            }


        }
    }
}
