using FluentValidation;

namespace DistanceCalculator.API.Models.Validators;

public class CalculateDistanceRequestValidator : AbstractValidator<CalculateDistanceRequest>
{
    public CalculateDistanceRequestValidator()
    {
        RuleFor(x => x.StartLocation)
            .NotEmpty()
            .SetValidator(x => new CoordinateModelValidator());
        RuleFor(x => x.EndLocation)
            .NotEmpty()
            .SetValidator(x => new CoordinateModelValidator());
    }
}