using System.Text.Json.Serialization;

namespace DistanceCalculator.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalculationType
{
    Cosine,
    Haversine
}