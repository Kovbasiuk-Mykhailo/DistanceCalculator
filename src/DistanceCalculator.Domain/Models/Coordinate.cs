namespace DistanceCalculator.Domain.Models;

public class Coordinate
{
    public Coordinate(double longitude, double latitude)
    {
        if (longitude is < -180 or > 180)
            throw new ArgumentException("Longitude value should be in (-180, 180) range.");
        
        if (latitude is < -90 or > 90)
            throw new ArgumentException("Latitude value should be in (-180, 180) range.");
        
        Longitude = longitude;
        Latitude = latitude;
    }

    public double Longitude { get; }

    public double Latitude { get; }
}