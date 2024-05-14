using DistanceCalculator.API.Models;
using DistanceCalculator.API.Models.Validators;
using FluentValidation.TestHelper;

namespace DistanceCalculator.API.Tests.Models.Validators;

public class CoordinateModelValidatorTests
{
    private readonly CoordinateModelValidator _validator = new();

    [Theory]
    [InlineData(-180, -90)]
    [InlineData(180, 90)]
    public void CoordinateModel_FailValidation(double longitude, double latitude)
    {
        var coordinate = new CoordinateModel { Longitude = longitude, Latitude = latitude };

        _validator.TestValidate(coordinate).ShouldHaveValidationErrorFor(x => x.Longitude);
        _validator.TestValidate(coordinate).ShouldHaveValidationErrorFor(x => x.Latitude);
    }

    [Theory]
    [InlineData(-179.99, 0)]
    [InlineData(179.99, 0)]
    [InlineData(0, -89.99)]
    [InlineData(0, 89.99)] 
    public void CoordinateModel_PassValidation(double longitude, double latitude)
    {
        var coordinate = new CoordinateModel { Longitude = longitude, Latitude = latitude };
        
        _validator.TestValidate(coordinate).ShouldNotHaveValidationErrorFor(x => x.Longitude);
        _validator.TestValidate(coordinate).ShouldNotHaveValidationErrorFor(x => x.Latitude);
    }
}