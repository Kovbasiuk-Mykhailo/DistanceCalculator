using DistanceCalculator.Domain.Calculators;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Tests.Calculators;

public class CosineDistanceCalculationStrategyTests
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)] // Same coordinates
    [InlineData(0, 0, 0, 90, Math.PI * Constants.Constants.EarthRadius / 2)] // North Pole
    [InlineData(40, 30, -30, -20, 9337093.84)] // Arbitrary coordinates
    [InlineData(0, 0, 0, 180, Constants.Constants.EarthRadius * Math.PI)] // Antipodal points
    public void Calculate_ReturnsCorrectDistance(
        double latitudeA,
        double longitudeA,
        double latitudeB,
        double longitudeB,
        double expectedDistance)
    {
        var strategy = new CosineDistanceCalculationStrategy();
        var coordinateA = new Coordinate(longitudeA, latitudeA);
        var coordinateB = new Coordinate(longitudeB, latitudeB);

        var distance = strategy.Calculate(coordinateA, coordinateB);

        distance.Should().BeApproximately(expectedDistance, 0.01);
    }
}