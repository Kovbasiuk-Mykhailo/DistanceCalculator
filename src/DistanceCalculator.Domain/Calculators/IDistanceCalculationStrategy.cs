using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Calculators;

public interface IDistanceCalculationStrategy
{
    public CalculationType Type { get; }
    
    public double Calculate(Coordinate coordinateA, Coordinate coordinateB);
}