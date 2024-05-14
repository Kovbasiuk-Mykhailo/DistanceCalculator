using DistanceCalculator.Domain.Extensions;

namespace DistanceCalculator.Domain.Tests.Extensions;

public class DistanceExtensionsTests
{
    [Fact]
    public void MetersToDistance_ConvertsMetersToDistance()
    {
        const double meters = 5000;
        
        var distance = meters.MetersToDistance();
        
        distance.Should().NotBeNull();
        distance.Meters.Should().Be(meters);
    }
}