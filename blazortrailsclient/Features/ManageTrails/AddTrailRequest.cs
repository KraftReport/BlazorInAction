using blazortrailsshared.Features.ManageTrails;
using FluentValidation;
using MediatR;

namespace blazortrailsclient.Features.ManageTrails
{
    public record AddTrailRequest(TrailDto TrailDto) : IRequest<AddTrailRequest.Response> 
    {
        public const string RouteTemplate = "/api/trails";

        public record Response(int TrailId);
    }

    public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
    {
        public AddTrailRequestValidator()
        {
            RuleFor(x => x.TrailDto).SetValidator(new TrailValidattor());
        }
    }
}
