using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Extensions;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Calculators;

public class DistanceCalculator(IEnumerable<IDistanceCalculationStrategy> distanceCalculationStrategies)
    : IDistanceCalculator
{
    public Distance Calculate(
        Coordinate coordinateA,
        Coordinate coordinateB,
        CalculationType calculationType = CalculationType.Cosine)
    {
        var distanceCalculationStrategy = distanceCalculationStrategies.Single(x => x.Type == calculationType);

        var distanceInMeters = distanceCalculationStrategy.Calculate(coordinateA, coordinateB);
        
        return distanceInMeters.MetersToDistance();
    }
}