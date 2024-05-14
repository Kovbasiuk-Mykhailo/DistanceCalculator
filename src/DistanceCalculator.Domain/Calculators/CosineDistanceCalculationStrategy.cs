using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Extensions;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Calculators;

/// <summary>
/// The simple spherical law of cosines formula (cos c = cos a cos b + sin a sin b cos C) gives well-conditioned results down to distances as small as a few metres on the earth’s surface.
/// (Note that the geodetic form of the law of cosines is rearranged from the canonical one so that the latitude can be used directly, rather than the colatitude).
/// This makes the simpler law of cosines a reasonable 1-line alternative to the haversine formula for many geodesy purposes (if not for astronomy).
/// </summary>
public class CosineDistanceCalculationStrategy : IDistanceCalculationStrategy
{
    public CalculationType Type => CalculationType.Cosine;
    
    public double Calculate(Coordinate coordinateA, Coordinate coordinateB)
    {
        var a = (90 - coordinateA.Latitude).ToRadians();
        var b = (90 - coordinateB.Latitude).ToRadians();
        var phi = (coordinateA.Longitude - coordinateB.Longitude).ToRadians();

        var d = Math.Acos(Math.Cos(a)*Math.Cos(b) + Math.Sin(a)*Math.Sin(b)*Math.Cos(phi));

        var distance = Constants.Constants.EarthRadius * d;

        return distance;
    }
}