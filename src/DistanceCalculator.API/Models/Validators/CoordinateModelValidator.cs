using FluentValidation;

namespace DistanceCalculator.API.Models.Validators;

public class CoordinateModelValidator : AbstractValidator<CoordinateModel>
{
    public CoordinateModelValidator()
    {
        RuleFor(x => x.Longitude)
            .ExclusiveBetween(-180, 180);
        RuleFor(x => x.Latitude)
            .ExclusiveBetween(-90, 90);
    }
}