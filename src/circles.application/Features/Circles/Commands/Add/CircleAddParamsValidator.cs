

using circles.api.contracts.Circles.Commands.Add;
using FluentValidation;

namespace circles.application.Features.Circles.Commands.Add;

public class CircleAddParamsValidator : AbstractValidator<CircleAddParams>
{
    public CircleAddParamsValidator()
    {
        RuleFor(x => x.Denomination)
            .NotEmpty().WithMessage("Denomination is required.")
            .Length(3, 50).WithMessage("Denomination must be between 3 and 50 characters.");
    }
}
