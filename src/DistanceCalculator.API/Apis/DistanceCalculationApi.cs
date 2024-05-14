using DistanceCalculator.API.Enums;
using DistanceCalculator.API.Filters;
using DistanceCalculator.API.Models;
using DistanceCalculator.API.Services;
using DistanceCalculator.Domain.Calculators;
using DistanceCalculator.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculator.API.Apis;

public static class DistanceCalculationApi
{
    public static IEndpointRouteBuilder MapDistanceCalculationApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/calculateDistance", CalculateDistance)
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

        return app;
    }

    private static Results<Ok<DistanceModel>, UnprocessableEntity> CalculateDistance(
        [FromBody, Validate] CalculateDistanceRequest request,
        IDistanceCalculator distanceCalculator,
        ILocalizationService localizationService)
    {
        var start = new Coordinate(request.StartLocation.Longitude, request.StartLocation.Latitude);
        var end = new Coordinate(request.EndLocation.Longitude, request.EndLocation.Latitude);

        var distance = distanceCalculator.Calculate(start, end, request.CalculationType);

        var preferredUnitOfMeasure = localizationService.GetDistanceUnitOfMeasure();
        var result = new DistanceModel
        {
            UnitOfMeasure = preferredUnitOfMeasure.ToString(),
            Distance = preferredUnitOfMeasure switch
            {
                DistanceUnitOfMeasure.Meters => distance.Meters,
                DistanceUnitOfMeasure.Kilometers => distance.Kilometers,
                DistanceUnitOfMeasure.Miles => distance.Miles
            }
        };

        return TypedResults.Ok(result);
    }
}