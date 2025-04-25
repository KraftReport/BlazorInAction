using blazortrailsshared.Features.ManageTrails.Shared;
using MediatR;
using System.Net.Http.Json;

namespace blazortrailsclient.Features.Home.Shared
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
    {

        private readonly HttpClient _httpClient;

        public GetTrailsHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<GetTrailsRequest.Response>($"{GetTrailsRequest.RouteTemplate}", cancellationToken);

            if (response is null)
            {
                throw new Exception("Failed to get trails");
            }

            return response;
        }
    }
}
