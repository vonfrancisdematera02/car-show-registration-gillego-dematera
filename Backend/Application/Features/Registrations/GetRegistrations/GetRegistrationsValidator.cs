using FluentValidation;
namespace Application.Features.Registrations.GetRegistrations;

public sealed class GetRegistrationsQueryValidator
    : AbstractValidator<GetRegistrationsQuery>
{
    public GetRegistrationsQueryValidator()
    {
        RuleFor(x => x.ParticipantName)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.ParticipantName));


        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be at least 1.");


        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50);
    }
}
