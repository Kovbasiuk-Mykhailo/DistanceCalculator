using DistanceCalculator.API.Enums;

namespace DistanceCalculator.API.Services;

public interface ILocalizationService
{
    public DistanceUnitOfMeasure GetDistanceUnitOfMeasure();
}