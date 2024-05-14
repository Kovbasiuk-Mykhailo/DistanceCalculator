using DistanceCalculator.Domain.Extensions;

namespace DistanceCalculator.Domain.Tests.Extensions;

public class DegreesExtensionsTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(90, Math.PI / 2)]
    [InlineData(180, Math.PI)]
    [InlineData(270, 3 * Math.PI / 2)]
    [InlineData(360, 2 * Math.PI)]
    public void ToRadians_ConvertsDegreesToRadiansCorrectly(double degrees, double expectedRadians)
    {
        var radians = degrees.ToRadians();
        
        radians.Should().BeApproximately(expectedRadians, 10E-10); // Using approximate comparison due to floating-point arithmetic
    }
}