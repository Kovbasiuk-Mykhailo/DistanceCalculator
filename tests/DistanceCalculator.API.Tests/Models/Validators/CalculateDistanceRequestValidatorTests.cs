using DistanceCalculator.API.Models;
using DistanceCalculator.API.Models.Validators;
using FluentValidation.TestHelper;

namespace DistanceCalculator.API.Tests.Models.Validators;

public class CalculateDistanceRequestValidatorTests
{
    private readonly CalculateDistanceRequestValidator _validator = new();
    
    [Fact]
    public void StartLocation_ShouldHaveCoordinateModelValidator()
    {
        _validator.ShouldHaveChildValidator(x => x.StartLocation, typeof(CoordinateModelValidator));
    }

    [Fact]
    public void EndLocation_ShouldHaveCoordinateModelValidator()
    {
        _validator.ShouldHaveChildValidator(x => x.EndLocation, typeof(CoordinateModelValidator));
    }

    [Fact]
    public void StartLocation_ShouldBeNotEmpty()
    {
        var request = new CalculateDistanceRequest { StartLocation = null! };

        _validator.TestValidate(request).ShouldHaveValidationErrorFor(x => x.StartLocation);
    }

    [Fact]
    public void EndLocation_ShouldBeNotEmpty()
    {
        var request = new CalculateDistanceRequest { EndLocation = null! };

        _validator.TestValidate(request).ShouldHaveValidationErrorFor(x => x.EndLocation);
    }
}