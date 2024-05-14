using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Extensions;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Calculators;

/// <summary>
/// The Haversine formula calculates the shortest distance between two points on a sphere using their latitudes and longitudes
/// measured along the surface.
/// </summary>
public class HaversineDistanceCalculationStrategy : IDistanceCalculationStrategy
{
    public CalculationType Type => CalculationType.Haversine;

    public double Calculate(Coordinate coordinateA, Coordinate coordinateB)
    {
        var phi1 = coordinateA.Latitude.ToRadians();
        var phi2 = coordinateB.Latitude.ToRadians();

        var deltaPhi = (coordinateB.Latitude - coordinateA.Latitude).ToRadians();
        var deltaLambda = (coordinateB.Longitude - coordinateA.Longitude).ToRadians();

        var a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                Math.Cos(phi1) * Math.Cos(phi2) * Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var distance = Constants.Constants.EarthRadius * c;

        return distance;
    }
}