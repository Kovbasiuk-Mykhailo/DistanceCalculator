using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Tests.Models;

public class CoordinateTests
{
    [Theory]
    [InlineData(-200, 0)]
    [InlineData(200, 0)]
    [InlineData(0, -100)]
    [InlineData(0, 100)]
    public void Constructor_InvalidCoordinates_ThrowsArgumentException(double longitude, double latitude)
    {
        var action = () => { _ = new Coordinate(longitude, latitude); };
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Constructor_ValidCoordinates_SetsPropertiesCorrectly()
    {
        const double longitude = 10;
        const double latitude = 20;
        
        var coordinate = new Coordinate(longitude, latitude);
        
        coordinate.Longitude.Should().Be(longitude);
        coordinate.Latitude.Should().Be(latitude);
    }
}