using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Calculators;

public interface IDistanceCalculator
{
    Distance Calculate(
        Coordinate coordinateA,
        Coordinate coordinateB,
        CalculationType calculationType = CalculationType.Cosine);
}