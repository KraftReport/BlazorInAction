using blazortrailsshared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http.Json;

namespace blazortrailsclient.Features.ManageTrails.EditTrail
{
    public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public EditTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return new EditTrailRequest.Response(true);
                }

                return new EditTrailRequest.Response(false);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new EditTrailRequest.Response(false);
            }
        }
    }
}
