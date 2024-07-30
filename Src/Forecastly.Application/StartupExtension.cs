using Forecastly.Application.Location;
using Forecastly.Application.Weather;
using Microsoft.Extensions.DependencyInjection;

namespace Forecastly.Application;

public static class StartupExtension
{
    public static IServiceCollection AddOpenWeatherServices(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>()
            .AddScoped<ILocationService, LocationService>();
        return services;
    }
}