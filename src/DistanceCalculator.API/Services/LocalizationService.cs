using DistanceCalculator.API.Constants;
using DistanceCalculator.API.Enums;
using Microsoft.AspNetCore.Localization;

namespace DistanceCalculator.API.Services;

public class LocalizationService(IHttpContextAccessor httpContextAccessor) : ILocalizationService
{
    public DistanceUnitOfMeasure GetDistanceUnitOfMeasure()
    {
        var requestCulture = httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture;

        return requestCulture!.Name switch
        {
            Localization.Cultures.EnUS => DistanceUnitOfMeasure.Kilometers,
            Localization.Cultures.EnGB => DistanceUnitOfMeasure.Miles,
            _ => DistanceUnitOfMeasure.Meters
        };
    }
}