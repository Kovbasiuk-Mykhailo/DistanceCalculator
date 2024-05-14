using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Extensions;

public static class DistanceExtensions
{
    public static Distance MetersToDistance(this double meters)
    {
        return Distance.FromMeters(meters);
    }
}