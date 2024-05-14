using System.ComponentModel.DataAnnotations;
using DistanceCalculator.Domain.Enums;

namespace DistanceCalculator.API.Models;

public class CalculateDistanceRequest
{
    [EnumDataType(typeof(CalculationType))]
    public CalculationType CalculationType { get; set; } = CalculationType.Cosine;

    public CoordinateModel StartLocation { get; set; } = null!;

    public CoordinateModel EndLocation { get; set; } = null!;
}