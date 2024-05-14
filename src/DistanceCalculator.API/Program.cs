using DistanceCalculator.API.Apis;
using DistanceCalculator.API.Constants;
using DistanceCalculator.API.Services;
using DistanceCalculator.Domain.Calculators;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)
    .AddFluentValidationRulesToSwagger();

builder.Services.AddScoped<IDistanceCalculator, DistanceCalculator.Domain.Calculators.DistanceCalculator>();
builder.Services.AddScoped<IDistanceCalculationStrategy, CosineDistanceCalculationStrategy>();
builder.Services.AddScoped<IDistanceCalculationStrategy, HaversineDistanceCalculationStrategy>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var settings = builder.Configuration.GetSection("Localization")
        .Get<DistanceCalculator.API.Configuration.Localization>();

    if (!Localization.SupportedCultures.Contains(settings!.DefaultCulture))
    {
        throw new NotSupportedException("Unsupported default culture value.");
    }

    options.SetDefaultCulture(settings.DefaultCulture)
        .AddSupportedCultures(Localization.SupportedCultures);
});

var app = builder.Build();
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDistanceCalculationApi();

app.Run();

public partial class Program { }