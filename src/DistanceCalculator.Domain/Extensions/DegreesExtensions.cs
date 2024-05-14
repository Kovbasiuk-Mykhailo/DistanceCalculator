namespace DistanceCalculator.Domain.Extensions;

public static class DegreesExtensions
{
    public static double ToRadians(this double degrees)
    {
        return degrees * Math.PI / 180;
    }
}