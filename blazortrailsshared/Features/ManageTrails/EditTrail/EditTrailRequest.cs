using blazortrailsshared.Features.ManageTrails.Shared;
using FluentValidation;
using MediatR; 

namespace blazortrailsshared.Features.ManageTrails.EditTrail
{
    public record EditTrailRequest(TrailDto TrailDto) : IRequest<EditTrailRequest.Response>
    {
        public const string RouteTemplate = "api/trails";
        public record Response(bool IsSuccess);
    }

    public class EditTrailRequestValidator : AbstractValidator<EditTrailRequest>
    {
        public EditTrailRequestValidator()
        {
            RuleFor(x => x.TrailDto).SetValidator(new TrailValidattor());
        }
    }
}
