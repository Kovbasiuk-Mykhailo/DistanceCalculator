using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Tests.Models;

public class DistanceTests
{
    [Fact]
    public void FromMeters_NegativeValue_ThrowsArgumentException()
    {
        const double meters = -100.1;
        
        Action action = () => Distance.FromMeters(meters);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Distance must be greater then or equal to 0.");
    }
    
    [Fact]
    public void FromMeters_ReturnsCorrectDistance()
    {
        const double meters = 5000.01;
        
        var distance = Distance.FromMeters(meters);
        
        distance.Kilometers.Should().Be(5.0000100000000005);
    }
    
    [Fact]
    public void Miles_ReturnsCorrectValue()
    {
        var distance = Distance.FromMeters(10 * 1000);
        
        distance.Miles.Should().BeApproximately(6.21371, 5);
    }
    
    [Fact]
    public void Meters_ReturnsCorrectValue()
    {
        var distance = Distance.FromMeters(10 * 1000);
        
        distance.Meters.Should().Be(10000);
    }
    
    [Fact]
    public void Kilometers_ReturnsCorrectValue()
    {
        var distance = Distance.FromMeters(10 * 1000);
        
        distance.Kilometers.Should().Be(10);
    }
}