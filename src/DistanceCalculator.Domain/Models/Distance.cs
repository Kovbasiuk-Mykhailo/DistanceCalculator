namespace DistanceCalculator.Domain.Models;

public class Distance
{
    private const double MilesInKilometer = 0.621371192;
    
    private Distance(double kilometers)
    {
        Kilometers = kilometers;
    }

    public double Kilometers { get; }

    public double Meters => Kilometers * 1000.0;
    
    public double Miles => Kilometers * MilesInKilometer;

    public static Distance FromMeters(double meters)
    {
        if (meters < 0)
            throw new ArgumentException($"Distance must be greater then or equal to 0.");
        
        return new Distance(meters / 1000.0);
    }
}