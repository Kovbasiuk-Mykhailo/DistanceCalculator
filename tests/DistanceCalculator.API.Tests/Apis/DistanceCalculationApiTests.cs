using System.Net;
using System.Text;
using DistanceCalculator.API.Constants;
using DistanceCalculator.API.Enums;
using DistanceCalculator.API.Models;
using DistanceCalculator.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace DistanceCalculator.API.Tests.Apis;

public class DistanceCalculationApiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private const string EndpointUrl = "/calculateDistance";
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CalculateDistance_InvalidRequestData_ReturnsUnprocessableEntityResult()
    {
        var requestContent = new StringContent(
            JsonConvert.SerializeObject(new CalculateDistanceRequest
            {
                EndLocation = new CoordinateModel { Longitude = 1, Latitude = 1 }
            }),
            Encoding.UTF8,
            "application/json");;
        
        var response = await _client.PostAsync(EndpointUrl, requestContent);
        
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
    
    [Fact]
    public async Task CalculateDistance_DefaultCulture_ReturnsOkResultInKilometers()
    {
        var requestContent = GetRequestContent();
        
        var response = await _client.PostAsync(EndpointUrl, requestContent);
        
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DistanceModel>(responseContent);
        result.Should().NotBeNull();
        result!.UnitOfMeasure.Should().Be(DistanceUnitOfMeasure.Kilometers.ToString());
    }

    [Fact]
    public async Task CalculateDistance_ProvidedCulture_ReturnsOkResultInCorrectUnitOfMeasure()
    {
        var requestContent = GetRequestContent();
        _client.DefaultRequestHeaders.Add(HeaderNames.AcceptLanguage, Localization.Cultures.EnGB);
        
        var response = await _client.PostAsync(EndpointUrl, requestContent);
        
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DistanceModel>(responseContent);
        result.Should().NotBeNull();
        result!.UnitOfMeasure.Should().Be(DistanceUnitOfMeasure.Miles.ToString());
    }
    
    private static StringContent GetRequestContent()
    {
        var requestContent = new StringContent(
            JsonConvert.SerializeObject(new CalculateDistanceRequest
            {
                StartLocation = new CoordinateModel { Longitude = 0, Latitude = 0 },
                EndLocation = new CoordinateModel { Longitude = 1, Latitude = 1 },
                CalculationType = CalculationType.Cosine
            }),
            Encoding.UTF8,
            "application/json");
        return requestContent;
    }
}