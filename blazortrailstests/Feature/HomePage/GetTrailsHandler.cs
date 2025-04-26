using AutoFixture;
using blazortrailsshared.Features.ManageTrails.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blazortrailstests.Feature.HomePage
{
    public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
    {
        public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
        {
            var fixture = new Fixture();
            var trails = fixture.CreateMany<GetTrailsRequest.Trail>();
            return  new GetTrailsRequest.Response(trails);
        }
    }
}
